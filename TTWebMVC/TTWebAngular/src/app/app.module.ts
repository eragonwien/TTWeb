import { NgModule, APP_INITIALIZER, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SettingService } from './services/setting.service';
import { LoginActivateGuard } from './guards/logginActivate.guard';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { GlobalErrorHandlerService } from './services/globalErrorHandler.service';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ApiService } from './services/api.service';
import { AuthService } from './services/auth.service';
import { FormService } from './services/form.service';
import { NavbarComponent } from './navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ScheduleJobDefFormComponent } from './schedule-job-def-form/schedule-job-def-form.component';
import { ModelStateErrorNotificationComponent } from './shared/model-state-error-notification/model-state-error-notification.component';

@NgModule({
  declarations: [AppComponent, LoginComponent, HomeComponent, UserProfileComponent, NavbarComponent, ScheduleJobDefFormComponent, ModelStateErrorNotificationComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
  ],
  providers: [
    SettingService,
    { provide: APP_INITIALIZER, useFactory: loadSettings, deps: [SettingService], multi: true },
    LoginActivateGuard,
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandlerService,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
    ApiService,
    AuthService,
    FormService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function loadSettings(settingService: SettingService) {
  return () => settingService.loadRuntimeSettings();
}
