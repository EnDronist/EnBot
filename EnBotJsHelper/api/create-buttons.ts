import { Validation } from '@utils/types';

export type CreateButtonsAPI = {
    // Data from client to server (post)
    req: {
        channel: string;
        content: string;
        buttons: Array<{
            text: string;
            color: string;
        }>;
    }
    // Data from server to client
    res: { }
}

export const verification: Validation<CreateButtonsAPI['req']> = {
    channel: value => typeof(value) == "string",
    content: value => typeof(value) == "string",
    buttons: value => {
        if (typeof(value) != "object") return false;
        var isValid = true;
        value.forEach(elem => {
            if (!(typeof(elem.text) == "string" || /^.{1, 64}$/.test(elem.text)))
                isValid = false;
            else if (!(typeof(elem.color) == "string" || /^(blurple|grey|green|red)$/.test(elem.color)))
                isValid = false;
        });
        return isValid;
    },
}