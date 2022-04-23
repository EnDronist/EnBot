export function fromUrlParams(value: string): Array<string|number> {
    var iterator = new URLSearchParams(value).entries();
    var result = [];
    var iteratorResult: IteratorResult<[string, string], [string, string]>;
    while (!(iteratorResult = iterator.next()).done)
        result[iteratorResult.value[0]] = isNaN(+iteratorResult.value[1])
            ? iteratorResult.value[1]
            : +iteratorResult.value[1];
    return result;
}
export function toUrlParams(object: Object): string {
    return '?' + Object.entries(object).map(
        ([key, value]) => `${key}=${value}`
    ).join('&');
}
export function mergeUrlParams(lhs: string, rhs: string): string {
    return toUrlParams(Object.assign(fromUrlParams(lhs), fromUrlParams(rhs)));
}