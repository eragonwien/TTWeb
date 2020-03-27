import { HttpClient } from '@angular/common/http';
import { RuntimeSettings } from './../models/runtime.settings';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SettingService {
  public runtimeSettings: RuntimeSettings;

  constructor(private http: HttpClient) {
    this.loadRuntimeSettings();
  }

  loadRuntimeSettings() {
    return this.http.get('/assets/runtime.settings.json').subscribe((data: RuntimeSettings) => {
      this.runtimeSettings = data;
    });
  }
}
