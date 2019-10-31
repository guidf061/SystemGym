import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";

import { Token } from '../../models/token-model';
import { LoggedUserService } from './logged-user.service';

import { CustomErrorHandler } from './custom-error-handler';
import { JwtService } from '..';
import { Usuario } from '../../models/usuario-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  redirectUrl: string;

  url = 'https://localhost:44365/api/Usuario';
  private endpointUrl = this.url;  // URL to web API

  constructor(private http: HttpClient,
    private jwtService: JwtService,
    private loggedUserService: LoggedUserService) { }

  login(userName: string, password: string): Promise<boolean> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'q=0.8;application/json;q=0.9');
    headers = headers.set('Content-Type', 'application/x-www-form-urlencoded');

    let obj = { userName: userName, password: password };
    let body = this.serializeObj(obj);

    return this.http.post<Usuario>(this.endpointUrl + '/Login', obj)
    .toPromise()
    .then(res => { return true; })
    .catch(CustomErrorHandler.handleApiError)
  }

  loggedIn(): boolean {
    return this.tokenNotExpired();
  }

  private serializeObj(obj) {
    let result = [];
    for (let property in obj) {
      result.push(encodeURIComponent(property) + '=' + encodeURIComponent(obj[property]));
    }
    return result.join('&');
  }

  private extractData(token: Token): boolean {
    let accessToken: string = token.access_token;
    let refreshToken: string = token.refresh_token;

    if (accessToken !== undefined && accessToken !== null && accessToken !== '') {
      localStorage.setItem('auth_token', accessToken);

      if (refreshToken !== undefined && refreshToken !== null && refreshToken !== '') {
        localStorage.setItem('refresh_token', refreshToken);
      }

      return true;
    } else {
      return false;
    }
  }

  private tokenNotExpired(): boolean {
    const token: string = localStorage.getItem('auth_token');
    const jwtService = new JwtService();

    return token !== undefined && token !== null && !jwtService.isTokenExpired(token);
  }
}
