import Server from '@server/Server';
import Express, { NextFunction, Response, Request } from 'express';
import asyncHandler from 'express-async-handler';
import { AuthorizationAPI, verification } from '@api/authorization';
import validate from '@utils/validate';
import 'colors';

export default function(app: Express.Application): void {
    var handler = app.get('handler') as typeof asyncHandler;
    app.post(/.*/, handler(async (req: Request, res: Response, next: NextFunction) => {
        // Body validation
        let reqBody = req.query as AuthorizationAPI['req'];
        if (!validate(reqBody, verification)) {
            res.sendStatus(400 /* Bad request */);
            Server.runtimeInfo[req.id].isSending = true;
            next(); return;
        }
        // Checking token
        if (reqBody.token != app.get('token')) {
            res.sendStatus(401 /* Unauthorized */);
            Server.runtimeInfo[req.id].isSending = true;
            next(); return;
        }
        // Next route
        next();
    }));
}