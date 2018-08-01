import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './_services/auth.service';
import { StorageService } from './_services/storage.service';
import { UserService } from './_services/user.service';
import { LinkService } from './_services/link.service';
import { VisitorService } from './_services/visitor.service';
import { ApiInterceptor } from './_helpers/api.interceptor';
import { AppComponent } from './app.component';
import { RedirectComponent } from './redirect/redirect.component';

@NgModule({
  declarations: [
    AppComponent,
    RedirectComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    StorageService,
    AuthService,
    UserService,
    LinkService,
    VisitorService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
