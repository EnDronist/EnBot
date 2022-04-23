/* Imports */
// Server
import Express, { Request, Response, NextFunction, RequestHandler } from 'express';
import https from 'https';
import bodyParser from 'body-parser';
import cookieParser from 'cookie-parser';
import session from 'express-session';
import requestId from 'express-request-id';
import asyncHandler from 'express-async-handler';
// Routes
import authorization from '@routes/authorization';
import createButtons from '@routes/create-buttons';
// Get token
import getToken from './getToken';
// Discord
import Discord from 'discord.js';
import discordButton, { MessageComponent, ExtendedTextChannel } from 'discord-buttons';
import fetch from 'node-fetch';
import '@tensorflow/tfjs';
import tensorflow from '@tensorflow/tfjs-node';
// JSON
import serverConfig from './server-config.json';
// MySQL
import mysql from 'promise-mysql';
// Utilities
import fs from 'fs';
import http from 'http';
import path from 'path';
import minimist from 'minimist';
import { fromUrlParams, toUrlParams } from '@utils/url-params';
import 'colors';
import { HTTPOptions } from 'discord.js';

// Get command line arguments
var argv = minimist(process.argv.slice(2));
// Argument "with_errors" makes server unstable to errors (for debug)
var handler: (handler: RequestHandler) => RequestHandler;
if (argv._.includes('with_errors')) {
    console.log('"with_errors" is enabled');
    handler = (handler: RequestHandler): RequestHandler => (handler)
}
else handler = asyncHandler;
// Argument "post" indicates the port that the server will listen
var port: number = 3000; // default
if (argv['port'] != undefined) {
    port = argv['port'];
    console.log('"port" specified by ' + port);
}

export default class Server {
    // Fields
    public static runtimeInfo: {
        [key in Request['id']]: {
            startTime?: number,
            isSending: boolean,
            endTime?: number,
        }
    } = {};
    public readonly postAddress = "https://localhost:3001";
    public readonly postOptions = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    };
    public readonly httpOptions = {
        host: 'localhost', port: 3001, path:'/', method: 'POST'
    };
    // Methods
    constructor() {}
    async run(): Promise<void> {
        // MySQL connecting
        console.log("Connecting to database...");
        var database: mysql.Connection = await mysql.createConnection(serverConfig.database)
            .catch(err => { console.log("Connecting failed: " + err.message); return null; });
        if (!database) return;
        console.log("Connection established successfully");
        // Creating app
        const app = Express();

        // Filling handlers
        app.set('path', path);
        app.set('fs', fs);
        app.set('mysql', mysql);
        app.set('database', database);
        app.set('handler', handler);
        // Save token
        var token = getToken();
        if (!token) {
            console.log("Token not found".red);
            return;
        }
        app.set('token', token);

        // Discord client
        //let model_path = 'file://./model/nietzsche.json';
        //(tensorflow as any).loadModel(model_path)
        //    .then(model => {
        //        model.summary();
        //    }
        //)
        //.catch(error => {
        //    console.error(error)
        //});
        const discordClient = new Discord.Client();
        discordClient.on("ready", async () => {
            console.log(`Logged in as ${discordClient.user.tag}!`);
        });
        discordClient.on("clickButton", async (button: MessageComponent) => {
            var buttonInfo : {
                id: number;
                discordID: number;
                guild: {
                    id: number;
                    name: string;
                    ownerID: number;
                };
                channel: {
                    id: number;
                    name: string;
                    nsfw: boolean;
                };
                clicker: {
                    id: number;
                };
                message: {
                    id: number;
                    content: string;
                    author: {
                        id: number;
                        username: string;
                        bot: boolean;
                    };
                    createdTimestamp: number;
                };
            } = {
                id: parseInt(button.id),
                discordID: parseInt(button.discordID),
                guild: {
                    id: parseInt(button.guild.id),
                    name: button.guild.name,
                    ownerID: parseInt(button.guild.ownerID),
                },
                channel: {
                    id: parseInt(button.channel.id),
                    name: (button.channel as ExtendedTextChannel).name,
                    nsfw: (button.channel as ExtendedTextChannel).nsfw,
                },
                clicker: {
                    id: parseInt(button.clicker.id),
                },
                message: {
                    id: parseInt(button.message.id),
                    author: {
                        id: parseInt(button.message.author.id),
                        username: button.message.author.username,
                        bot: button.message.author.bot,
                    },
                    content: button.message.content,
                    createdTimestamp: button.message.createdTimestamp,
                },
            };
            try {
                //await fetch(this.postAddress + "/endungeons", {
                //    ...this.postOptions,
                //    body: JSON.stringify(buttonInfo),
                //});
                //var options = {
                //    host: 'localhost', port: 8080, path:'/', method: 'POST'
                //}
                //// Сейчас проблема тут
                var request = http.request(this.httpOptions, function(response) {
                    response.on('data', function(data) {
                        console.log(data);
                    });
                });
                request.write(JSON.stringify(buttonInfo));
                request.end();
                console.log(`${"DiscordClient".blue}: ${"\"clickButton\" message was sent to bot server".green}.`);
            }
            catch (e) {
                console.log(`${"DiscordClient".blue}: ${"server is offline or unavailable".red}. ${e}`);
            }
        });
        discordClient.login(token);
        discordButton(discordClient);
        app.set('discordClient', discordClient);
        app.set('discordButton', discordButton);

        // Public folder
        app.use('/public', Express.static('../public'));
        app.use('/favicon.ico', Express.static('../public/favicon.ico'));

        // Parsers
        app.use(bodyParser.urlencoded({ extended: true }));
        app.use(bodyParser.json());
        app.use(cookieParser());
        app.use(requestId());

        // Other
        app.use(session({
            secret: 'secret',
            resave: true,
            saveUninitialized: true,
        }));

        // Start
        app.use(/.*/, async (req: Request, res: Response, next: NextFunction) => {
            Server.runtimeInfo[req.id] = {
                startTime: new Date().getTime(),
                isSending: false,
            };
            // Printing time
            process.stdout.write(new Date().toLocaleTimeString('ru').green + ': ');
            // Printing method
            process.stdout.write(req.method === 'GET' ? 'GET'.blue : 'POST'.blue);
            // Printing other
            var censoredUrl;
            if (req.originalUrl.search(/\?/) == -1)
                censoredUrl = req.originalUrl;
            else {
                var censoredParameters = fromUrlParams(req.originalUrl.substr(req.originalUrl.search(/\?/) + 1));
                if (censoredParameters["token"]) censoredParameters["token"] = "<censored>";
                censoredUrl = req.originalUrl.substr(0, req.originalUrl.search(/\?/)) + toUrlParams(censoredParameters);
            }
            process.stdout.write(` from ${req.socket.remoteAddress.magenta} to ${censoredUrl.cyan}\n`);
            // req.get('host'); // host IP-address
            // console.log('Cookies: ' + JSON.stringify(req.cookies)); // prints client cookies
            next();
        });

        // Authorization (must be first route)
        // authorization
        authorization(app);
        // create-buttons
        createButtons(app);

        // End
        app.get(/.*/, async (req: Request, res: Response, next: NextFunction) => {
            if (!Server.runtimeInfo[req.id].isSending)
                res.send(`<h1>Результат по запросу "${req.url}" не найден.</h1>`);
            next();
        });
        app.post(/.*/, async (req: Request, res: Response, next: NextFunction) => {
            if (!Server.runtimeInfo[req.id].isSending)
                res.sendStatus(405/* Method Not Allowed */);
            next();
        });
        app.use(/.*/, async (req: Request, res: Response) => {
            let currentInfo = Server.runtimeInfo[req.id];
            currentInfo.endTime = new Date().getTime();
            console.log(`Successfully sended [${res.statusCode}, ${currentInfo.endTime - currentInfo.startTime}ms]`);
            delete Server.runtimeInfo[req.id];
        });
        // End with error
        app.use(async (err: any, req: Request, res: Response, next: NextFunction) => {
            let currentInfo = Server.runtimeInfo[req.id];
            if (currentInfo == undefined) return;
            currentInfo.endTime = new Date().getTime();
            console.log("Error".red + ` [${currentInfo.endTime - currentInfo.startTime}ms]: ${err}`);
            delete Server.runtimeInfo[req.id];
            res.sendStatus(500);
        });
        // Listening port
        https.createServer({
            key: fs.readFileSync('../secure/localhost.key'),
            cert: fs.readFileSync('../secure/localhost.crt'),
        }, app)
        .listen(port, () => console.log(`Server running at https://localhost:${port}/`));
    }
}
// Starting server
new Server().run();