export class HttpRequestResult<T> {
    public data: T | null = null;
    public isSuccess: boolean = false;
    public errorMessages: string[] = [];
    public statusCode: number | undefined;
}