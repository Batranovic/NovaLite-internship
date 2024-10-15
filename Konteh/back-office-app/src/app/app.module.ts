import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MSAL_INSTANCE, MsalModule, MsalService } from '@azure/msal-angular'
import { IPublicClientApplication, PublicClientApplication } from '@azure/msal-browser';

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: '4e1ff54b-bf34-4f45-83ce-e50fc32967cd',
      authority: 'https://login.microsoftonline.com/common',
      redirectUri: 'http://localhost:4200',
    }
  })
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MsalModule
  ],
  providers: [
  {
    provide: MSAL_INSTANCE,
    useFactory: MSALInstanceFactory
  },
  MsalService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
