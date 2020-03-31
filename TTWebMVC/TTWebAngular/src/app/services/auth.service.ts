import { SettingService } from './setting.service';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { shareReplay, tap } from 'rxjs/operators';
import { LoginUser } from 'src/models/login.user';
import { throwError } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private accessTokenStore = 'access_token';
  private refreshTokenStore = 'refresh_token';
  private loginUserIdStore = 'login_id';
  constructor() {}

  public saveLoginToken(loginUser: LoginUser) {
    localStorage.setItem(this.loginUserIdStore, loginUser.id.toString());
    localStorage.setItem(this.accessTokenStore, loginUser.accessToken);
    localStorage.setItem(this.refreshTokenStore, loginUser.refreshToken);
  }

  public logout() {
    localStorage.removeItem(this.accessTokenStore);
    localStorage.removeItem(this.refreshTokenStore);
  }

  public isLoggedIn() {
    return localStorage.getItem(this.accessTokenStore) !== null;
  }

  public isLoggedOut() {
    return !this.isLoggedIn;
  }

  public getAccessToken() {
    return localStorage.getItem(this.accessTokenStore);
  }

  public getRefreshToken() {
    return localStorage.getItem(this.refreshTokenStore);
  }

  public getInjectedRequest(req: HttpRequest<any>) {
    const token = this.getAccessToken();
    if (!token) {
      return req;
    }
    return req.clone({
      setHeaders: {
        Authorization: 'Bearer ' + token,
      },
    });
  }
}
