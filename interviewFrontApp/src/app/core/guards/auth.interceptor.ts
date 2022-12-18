import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { delay, finalize, Observable } from "rxjs";
import { LoadingService } from "../utilities/loading.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(
        private loading: LoadingService
    ) {
    }

    intercept(req: HttpRequest<any>, httpshandler: HttpHandler): Observable<HttpEvent<any>> {



        // set token here

        // var request = req.clone({
        //     headers: req.headers.set("Authorization", `Bearer ${token}`)
        // });

        var request = req.clone();

        this.loading.show();

        return httpshandler.handle(request).pipe(
            delay(500),
            finalize(() => this.loading.hide())
        );
    }

}