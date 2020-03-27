import { SettingService } from './setting.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { shareReplay, tap } from 'rxjs/operators';
import { LoginUser } from 'src/models/login.user';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private accessTokenStore = 'access_token';
  private refreshTokenStore = 'refresh_token';
  constructor(private http: HttpClient, private appSettings: SettingService) {}

  login(email: string, password: string) {
    return this.http
      .post(this.appSettings.runtimeSettings.apiAuthenticateRoute, { email, password })
      .pipe(tap((loginUser: LoginUser) => this.saveLoginToken(loginUser)))
      .pipe(shareReplay(1));
  }

  private saveLoginToken(loginUser: LoginUser) {
    localStorage.setItem(this.accessTokenStore, loginUser.accessToken);
    localStorage.setItem(this.refreshTokenStore, loginUser.refreshToken);
  }

  public logout() {
    localStorage.removeItem(this.accessTokenStore);
    localStorage.removeItem(this.refreshTokenStore);
  }

  public reauthenticate() {
    const refreshToken = this.getRefreshToken();
    if (refreshToken === null) {
      throwError('refresh token is empty');
    }
    return this.http
      .post(this.appSettings.runtimeSettings.apiReauthenticateRoute, {
        accessToken: this.getAccessToken(),
        refreshToken,
      })
      .pipe(tap((loginUser) => this.saveLoginToken))
      .pipe(shareReplay(1));
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
}
