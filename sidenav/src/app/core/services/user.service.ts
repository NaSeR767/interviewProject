import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpRequestResult } from 'src/app/shared/models/http-request-result';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  public get(userId: string): Observable<HttpRequestResult<any>> {
    return this.http.get<HttpRequestResult<any>>(`${this.baseUrl}api/User/Get/${userId}`);
  }

}
