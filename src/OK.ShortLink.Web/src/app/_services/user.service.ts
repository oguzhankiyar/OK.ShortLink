import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../_models/user.model';

@Injectable()
export class UserService {
  private apiEndPoint: string = environment.apiEndpoint;

  constructor(private httpClient: HttpClient) {}

  public getList(): Subject<User[]> {
    const result: Subject<User[]> = new Subject<User[]>();

    this.httpClient.get(this.apiEndPoint + '/users').subscribe(
      (data: User[]) => {
        result.next(data);
      },
      err => {
        result.next(null);
      }
    );

    return result;
  }

  public getById(id: number): Subject<User> {
    const result: Subject<User> = new Subject<User>();

    this.httpClient.get<User>(this.apiEndPoint + '/users/' + id).subscribe(
      (data: User) => {
        result.next(data);
      },
      err => {
        result.next(null);
      }
    );

    return result;
  }

  public create(
    username: string,
    password: string,
    isActive: boolean
  ): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();
    const user: User = new User(null, username, password, isActive);

    this.httpClient.post(this.apiEndPoint + '/users', user).subscribe(
      data => {
        result.next(true);
      },
      err => {
        result.next(false);
      }
    );

    return result;
  }

  public edit(
    id: number,
    password: string,
    isActive: boolean
  ): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();
    const user: User = new User(id, null, password, isActive);

    this.httpClient.put(this.apiEndPoint + '/users/' + id, user).subscribe(
      data => {
        result.next(true);
      },
      err => {
        result.next(false);
      }
    );

    return result;
  }

  public delete(id: number): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();

    this.httpClient.delete(this.apiEndPoint + '/users/' + id).subscribe(
      data => {
        result.next(true);
      },
      err => {
        result.next(false);
      }
    );

    return result;
  }
}
