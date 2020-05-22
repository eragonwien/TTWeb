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

@NgModule({
  declarations: [AppComponent, LoginComponent, HomeComponent, UserProfileComponent, NavbarComponent],
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
    {
      provide: APP_INITIALIZER,
      multi: true,
      deps: [SettingService],
      useFactory: (settingService: SettingService) => {
        return () => {
          return settingService.loadRuntimeSettings();
        };
      },
    },
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
    SettingService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
