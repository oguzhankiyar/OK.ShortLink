import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { LinkService } from './../_services/link.service';

@Component({
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css']
})
export class RedirectComponent implements OnInit {
  public message: string = 'Loading...';

  constructor(
    private route: ActivatedRoute,
    private linkService: LinkService
  ) {}

  public ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const shortUrl: string = params['shortUrl'];

      this.linkService.getByShortUrl(shortUrl).subscribe(link => {
        if (link && link.originalUrl) {
          this.message = 'Redirecting...';

          window.location.href = link.originalUrl;
        } else {
          this.message = 'Not Found!';
        }
      });
    });
  }
}
