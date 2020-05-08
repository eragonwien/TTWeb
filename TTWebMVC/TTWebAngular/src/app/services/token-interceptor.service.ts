import { ApiService } from './api.service';
import { inject } from '@angular/core/testing';
import { Injectable, Inject, Injector } from '@angular/core';
import { catchError, filter, take, retryWhen, concatMap, tap, switchMap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject, OperatorFunction, concat } from 'rxjs';
import { LoginUser } from 'src/models/LoginUser.model';

@Injectable({
  providedIn: 'root',
})
export class TokenInterceptorService implements HttpInterceptor {
  private refreshingToken = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(private injector: Injector, private auth: AuthService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(this.auth.getInjectedRequest(req)).pipe(
      catchError((err) => {
        const refreshToken = this.auth.getRefreshToken();
        if (err instanceof HttpErrorResponse && err.status === 401 && refreshToken) {
          return this.hanleExpiredToken(req, next);
        }
        return throwError(err);
      })
    );
  }
  hanleExpiredToken(req: HttpRequest<any>, next: HttpHandler): Observable<any> {
    if (!this.refreshingToken) {
      this.refreshingToken = true;
      this.refreshTokenSubject.next(null);

      const api = this.injector.get(ApiService);
      return api.reauthenticate(this.auth.getAccessToken(), this.auth.getRefreshToken()).pipe(
        switchMap((user: LoginUser) => {
          this.refreshingToken = false;
          this.refreshTokenSubject.next(user.accessToken);
          this.auth.saveLoginToken(user);
          return next.handle(this.auth.getInjectedRequest(req));
        })
      );
    }
    return this.refreshTokenSubject.pipe(
      filter((token) => token != null),
      take(1),
      switchMap((token) => {
        return next.handle(this.auth.getInjectedRequest(req));
      })
    );
  }
}
