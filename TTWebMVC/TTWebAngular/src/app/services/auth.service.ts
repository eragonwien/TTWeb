import { SettingService } from './setting.service';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { shareReplay, tap } from 'rxjs/operators';
import { AppUser } from 'src/models/appUser.model';
import { throwError } from 'rxjs';
import { ApiService } from './api.service';

@Injectable()
export class AuthService {
  private accessTokenStore = 'access_token';
  private refreshTokenStore = 'refresh_token';
  private appUserIdStore = 'login_id';
  private appUserStore = 'login_user';
  constructor() {}

  public saveAppUser(appUser: AppUser) {
    if (appUser) {
      localStorage.setItem(this.appUserStore, JSON.stringify(appUser));
      localStorage.setItem(this.appUserIdStore, appUser.id.toString());
      localStorage.setItem(this.accessTokenStore, appUser.accessToken);
      localStorage.setItem(this.refreshTokenStore, appUser.refreshToken);
    }
  }

  public logout() {
    localStorage.removeItem(this.accessTokenStore);
    localStorage.removeItem(this.refreshTokenStore);
    localStorage.removeItem(this.appUserIdStore);
    localStorage.removeItem(this.appUserStore);
  }

  public getInjectedRequest(req: HttpRequest<any>) {
    const token = this.AccessToken;
    if (!token) {
      return req;
    }
    return req.clone({
      setHeaders: {
        Authorization: 'Bearer ' + token,
      },
    });
  }

  public get AppUser(): AppUser {
    return JSON.parse(localStorage.getItem(this.appUserStore));
  }

  public get Authenticated(): boolean {
    return localStorage.getItem(this.accessTokenStore) !== null && localStorage.getItem(this.appUserStore) !== null;
  }

  public get AccessToken(): string {
    return localStorage.getItem(this.accessTokenStore);
  }

  public get RefreshToken(): string {
    return localStorage.getItem(this.refreshTokenStore);
  }
}
