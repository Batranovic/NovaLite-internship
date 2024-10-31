import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ExamsModule } from './features/exams/exams.module';
import { AuthInterceptor } from './shared/auth-interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { UnauthorizedComponent } from './shared/unauthorized/unauthorized.component';
import { HttpUnauthorizedInterceptor } from './shared/unauthorized-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    UnauthorizedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ExamsModule
  ],
  providers: [
    provideAnimationsAsync(),
    { 
      provide: HTTP_INTERCEPTORS, 
      useClass: AuthInterceptor, 
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpUnauthorizedInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
