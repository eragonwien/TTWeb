import { HttpClient } from '@angular/common/http';
import { ApiSettings } from '../../models/api.settings';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SettingService {
  public api: ApiSettings;

  constructor(private http: HttpClient) {
    this.loadRuntimeSettings();
  }

  loadRuntimeSettings() {
    return this.http.get('/assets/api.settings.json').subscribe((data: ApiSettings) => {
      this.api = data;
    });
  }
}
