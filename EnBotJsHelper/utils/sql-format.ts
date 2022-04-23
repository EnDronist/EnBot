export default function sqlFormat(value: string): string {
    if (typeof(value) != 'string') return null;
    return value.replace('"', '\\"').replace("'", "\\'");
}