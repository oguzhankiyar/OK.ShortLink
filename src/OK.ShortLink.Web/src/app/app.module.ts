import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './_services/auth.service';
import { StorageService } from './_services/storage.service';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, HttpClientModule],
  providers: [StorageService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule {}
