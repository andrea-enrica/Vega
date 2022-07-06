import { ErrorHandler, Inject, Injectable } from "@angular/core";

@Injectable()
export class AppErrorHandler implements ErrorHandler
{
    handleError(error: any): void {
        // this.error('An unexpected error happened.', 'Error', {
        //     timeOut: 5000
        // });
    }
}