import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { Visitor } from '../_models/visitor.model';

@Injectable()
export class VisitorService {
  private apiEndPoint: string = environment.apiEndpoint;

  constructor(private httpClient: HttpClient) {}

  public getList(): Subject<Visitor[]> {
    const result: Subject<Visitor[]> = new Subject<Visitor[]>();

    this.httpClient.get(this.apiEndPoint + '/visitors').subscribe(
      (data: Visitor[]) => {
        result.next(data);
      },
      err => {
        result.next(null);
      }
    );

    return result;
  }

  public getById(id: number): Subject<Visitor> {
    const result: Subject<Visitor> = new Subject<Visitor>();

    this.httpClient
      .get<Visitor>(this.apiEndPoint + '/visitors/' + id)
      .subscribe(
        (data: Visitor) => {
          result.next(data);
        },
        err => {
          result.next(null);
        }
      );

    return result;
  }

  public create(
    linkId: number,
    ipAddress: string,
    userAgent: string,
    osInfo: string,
    deviceInfo: string,
    browserInfo: string
  ): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();
    const visitor: Visitor = new Visitor(
      null,
      linkId,
      null,
      ipAddress,
      userAgent,
      osInfo,
      deviceInfo,
      browserInfo,
      null,
      null
    );

    this.httpClient.post(this.apiEndPoint + '/visitors', visitor).subscribe(
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
