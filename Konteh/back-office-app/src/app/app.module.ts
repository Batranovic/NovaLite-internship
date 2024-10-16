import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NavBarComponent } from './features/layout/nav-bar/nav-bar.component';
import {LayoutModule} from "./features/layout/layout.module";
import {HttpClientModule} from '@angular/common/http';
import {CategoryNamePipe} from './features/questions/category-name.pipe';

@NgModule({
  declarations: [
    AppComponent,
  ],
    imports: [
        HttpClientModule,
        BrowserModule,
        AppRoutingModule,
        LayoutModule
    ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
