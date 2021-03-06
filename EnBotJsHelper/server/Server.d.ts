import Express from 'express';

export default class Server {
    static filesJSON: {
        [key: string]: any;
    };
    static runtimeInfo: {
        [key in Express.Request['sessionID']]: {
            startTime?: number;
            endTime?: number;
        };
    };
    constructor();
    run(): Promise<void>;
}