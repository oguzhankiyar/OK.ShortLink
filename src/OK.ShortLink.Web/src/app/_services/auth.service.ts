import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Token } from '../_models/token.model';
import { StorageService } from './storage.service';

@Injectable()
export class AuthService {
  private apiEndPoint: string = environment.apiEndpoint;
  private storageKey: string = 'authInfo';

  constructor(
    private httpClient: HttpClient,
    private storageService: StorageService
  ) {}

  public isAuthenticated(): boolean {
    const token = this.getToken();

    return token && token.length > 0;
  }

  public getToken(): string {
    const authInfo = this.storageService.get<Token>(this.storageKey);

    if (!authInfo || !authInfo.token || authInfo.expiresIn < new Date()) {
      return null;
    }

    return authInfo.token;
  }

  public login(username: string, password: string): Observable<Token> {
    const authRequest = {
      username: username,
      password: password
    };

    return this.httpClient
      .post<Token>(this.apiEndPoint + '/auth', authRequest)
      .pipe(
        map(authResponse => {
          if (authResponse && authResponse.token) {
            this.storageService.set<Token>(this.storageKey, authResponse);
          }

          return authResponse;
        })
      );
  }

  public logout() {
    this.storageService.del(this.storageKey);
  }
}
