import { Validation } from '@utils/types';

export type AuthorizationAPI = {
    // Data from client to server (get)
    req: {
        token: string;
    }
    // Data from server to client
    res: { }
}

export const verification: Validation<AuthorizationAPI['req']> = {
    token: value => value != null,
}