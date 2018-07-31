import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
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

  public login(username: string, password: string) {
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
