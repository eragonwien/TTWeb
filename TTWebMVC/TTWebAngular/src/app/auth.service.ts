import { SettingService } from './setting.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RuntimeSettings } from 'src/models/runtime.settings';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private appSettings: SettingService) {}

  login(email: string, password: string) {
    return this.http.post(this.appSettings.runtimeSettings.apiAuthenticateRoute, { email, password });
  }
}
