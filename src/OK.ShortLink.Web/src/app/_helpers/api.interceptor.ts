import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token: string = this.authService.getToken();

    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(request).pipe(
      tap(
        (event: HttpEvent<any>) => {},
        (err: any) => {
          this.handleError(err);
        }
      )
    );
  }

  private handleError(err: any): void {
    if (err instanceof HttpErrorResponse) {
      if (err.status === 400) {
        let message: string = 'Bad Request!';

        for (const field of Object.keys(err.error)) {
          message += '\n' + err.error[field].map(item => item + ' ');
        }

        alert(message);
      } else if (err.status === 401) {
        alert('Unauthorized! Please login!');
      } else if (err.status === 404) {
        alert('Not Found! Please try again!');
      } else {
        alert(err.message);
      }
    }
  }
}
