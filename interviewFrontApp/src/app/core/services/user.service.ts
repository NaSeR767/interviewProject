import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, lastValueFrom, Observable, shareReplay } from 'rxjs';
import { HttpRequestResult } from 'src/app/shared/models/http-request-result';
import { environment } from 'src/environments/environment';
import { IUser } from '../interfaces/iuser';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public baseUrl: string = environment.baseUrl;

  private userInfo = new BehaviorSubject<any>(null);

  constructor(private http: HttpClient) { }

  public get(userId: string): Observable<HttpRequestResult<any>> {
    return this.http.get<HttpRequestResult<any>>(`${this.baseUrl}api/User/Get/${userId}`);
  }

  public getPagination(pageNumber: number, take: number): Observable<HttpRequestResult<any>> {
    return this.http.get<HttpRequestResult<any>>(`${this.baseUrl}api/User/GetPagination/${pageNumber}/${take}`);
  }

  public create(user: IUser): Observable<HttpRequestResult<any>> {
    return this.http.post<HttpRequestResult<any>>(`${this.baseUrl}api/User/Create`, user);
  }

  public update(user: IUser): Observable<HttpRequestResult<any>> {
    return this.http.put<HttpRequestResult<any>>(`${this.baseUrl}api/User/Update`, user);
  }

  public delete(userId: string): Observable<HttpRequestResult<any>> {
    return this.http.delete<HttpRequestResult<any>>(`${this.baseUrl}api/User/Delete/${userId}`);
  }

  public nextUserInfo(userInfo: IUser) {
    this.userInfo.next(userInfo);
  }

  public getUserInfo() {
    return this.userInfo.asObservable();
  }


}
