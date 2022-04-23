import fs from "fs";

export default function(): string {
    var token = null;
    try {
        token = fs.readFileSync("../secure/token.txt", "utf8");
    }
    catch (e) {
        if (e) console.error(e);
    }
    return token;
}