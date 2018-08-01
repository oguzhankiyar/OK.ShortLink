import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { Link } from '../_models/link.model';

@Injectable()
export class LinkService {
  private apiEndPoint: string = environment.apiEndpoint;

  constructor(private httpClient: HttpClient) {}

  public getList(): Subject<Link[]> {
    const result: Subject<Link[]> = new Subject<Link[]>();

    this.httpClient.get(this.apiEndPoint + '/links').subscribe(
      (data: Link[]) => {
        result.next(data);
      },
      err => {
        result.next(null);
      }
    );

    return result;
  }

  public getById(id: number): Subject<Link> {
    const result: Subject<Link> = new Subject<Link>();

    this.httpClient.get<Link>(this.apiEndPoint + '/links/' + id).subscribe(
      (data: Link) => {
        result.next(data);
      },
      err => {
        result.next(null);
      }
    );

    return result;
  }

  public getByShortUrl(shortUrl: string): Subject<Link> {
    const result: Subject<Link> = new Subject<Link>();

    this.httpClient
      .get<Link>(this.apiEndPoint + '/links/byshorturl/' + shortUrl)
      .subscribe(
        (data: Link) => {
          result.next(data);
        },
        err => {
          result.next(null);
        }
      );

    return result;
  }

  public create(
    name: string,
    description: string,
    shortUrl: string,
    originalUrl: string,
    isActive: boolean
  ): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();
    const link: Link = new Link(
      null,
      null,
      null,
      name,
      description,
      shortUrl,
      originalUrl,
      isActive,
      null,
      null
    );

    this.httpClient.post(this.apiEndPoint + '/links', link).subscribe(
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
    name: string,
    description: string,
    shortUrl: string,
    originalUrl: string,
    isActive: boolean
  ): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();
    const link: Link = new Link(
      id,
      null,
      null,
      name,
      description,
      shortUrl,
      originalUrl,
      isActive,
      null,
      null
    );

    this.httpClient.put(this.apiEndPoint + '/links/' + id, link).subscribe(
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

    this.httpClient.delete(this.apiEndPoint + '/links/' + id).subscribe(
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
