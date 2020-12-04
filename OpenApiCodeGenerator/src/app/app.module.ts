import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContainerService } from './service/container.service';
import { DataService } from './service/http-data.service';
import { HttpClientModule } from '@angular/common/http';
import { StoreService } from './service/store.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [DataService, ContainerService, StoreService],
  bootstrap: [AppComponent]
})
export class AppModule { }
