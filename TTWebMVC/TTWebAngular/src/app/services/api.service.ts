import { shareReplay } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingService } from './setting.service';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient, private settings: SettingService) {}

  public login(email: string, password: string) {
    return this.http.post(this.settings.runtimeSettings.apiAuthenticateRoute, { email, password }).pipe(shareReplay(1));
  }

  public reauthenticate(accessToken: string, refreshToken: string) {
    return this.http
      .post(this.settings.runtimeSettings.apiReauthenticateRoute, { accessToken, refreshToken })
      .pipe(shareReplay(1));
  }

  public ping() {
    return this.http
      .post(
        `${this.settings.runtimeSettings.apiBaseUrl}/${this.settings.runtimeSettings.apiLoginUserController}/ping`,
        {}
      )
      .pipe(shareReplay(1));
  }
}
