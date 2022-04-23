import Server from '@server/Server';
import Express, { NextFunction, Response, Request } from 'express';
import asyncHandler from 'express-async-handler';
import { CreateButtonsAPI, verification } from '@api/create-buttons';
import validate from '@utils/validate';
import Discord from 'discord.js';
import 'colors';
import { MessageActionRow, MessageButton } from 'discord-buttons';

const urnRoot = "/create-buttons";

export default function(app: Express.Application): void {
    var handler = app.get('handler') as typeof asyncHandler;
    app.post(urnRoot, handler(async (req: Request, res: Response, next: NextFunction) => {
        // If response already sent
        if (Server.runtimeInfo[req.id].isSending) {
            next(); return;
        }
        // Body validation
        let reqBody = req.body as CreateButtonsAPI['req'];
        if (!validate(reqBody, verification)) {
            res.sendStatus(400/* Bad request */);
            Server.runtimeInfo[req.id].isSending = true;
            next(); return;
        }
        // Discord buttons creating
        var discordClient = app.get('discordClient') as Discord.Client;
        //var channel = discordClient.channels.cache.get(reqBody.channel.toString());
        //if (!channel)
        var channel = await discordClient.channels.fetch(reqBody.channel.toString());
        if (!channel) {
            res.sendStatus(404/* Not found */);
            Server.runtimeInfo[req.id].isSending = true;
            next(); return;
        }
        var readyButtons : Array<MessageButton> = [];
        var discordButton = app.get('discordButton');
        reqBody.buttons.forEach((button, i) => readyButtons.push(
            new (discordButton as any)
                .MessageButton()
                .setLabel(button.text)
                .setStyle(button.color) // blurple, grey, green, red
                .setID(i)
            )
        );
        var buttonsRow = new MessageActionRow().addComponents(...readyButtons);
        (channel as any).send(reqBody.content, { components: buttonsRow });
        // Sending responce
        res.sendStatus(200/* OK */);
        Server.runtimeInfo[req.id].isSending = true; next();
    }));
}