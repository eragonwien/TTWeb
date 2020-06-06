import { AppUser } from 'src/models/appUser.model';
import { shareReplay } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingService } from './setting.service';

@Injectable()
export class ApiService {
  constructor(private http: HttpClient, private settings: SettingService) {}

  public login(email: string, password: string) {
    return this.http.post(this.settings.api.authenticateRoute, { email, password });
  }

  public reauthenticate(accessToken: string, refreshToken: string) {
    return this.http.post(this.settings.api.reauthenticateRoute, { accessToken, refreshToken });
  }

  public ping() {
    return this.http.post(this.apiPathJoin([this.settings.api.base, this.settings.api.appUser, 'ping']), {});
  }

  public saveAppUser(appUser: AppUser) {
    return this.http.put(this.apiPathJoin([this.settings.api.base, this.settings.api.appUser, appUser.id]), appUser);
  }

  getScheduleList() {
    return this.http.get(this.apiPathJoin([this.settings.api.base, this.settings.api.scheduleJobDef]));
  }

  private apiPathJoin(parts: any[]) {
    return parts.join('/');
  }
}
