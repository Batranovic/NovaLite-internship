import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {LayoutModule} from "./features/layout/layout.module";
import {HttpClientModule} from '@angular/common/http';
import { QuestionsModule } from './features/questions/questions.module';
import { MSAL_INSTANCE, MSAL_INTERCEPTOR_CONFIG, MsalInterceptor, MsalInterceptorConfiguration, MsalModule, MsalService } from '@azure/msal-angular'
import { InteractionType, IPublicClientApplication, PublicClientApplication } from '@azure/msal-browser';
import { LayoutModule } from "./features/layout/layout.module";
import { FeaturesModule } from './features/features.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { environment } from './environments/environment';

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.msalConfig.clientId,
      authority: environment.msalConfig.authority,
      redirectUri: environment.msalConfig.redirectUri,
    }
  })
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  return {
    interactionType: InteractionType.Popup,
    protectedResourceMap: environment.msalInterceptorConfig.protectedResourceMap
  };
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MsalModule,
    LayoutModule,
    FeaturesModule,
    HttpClientModule,
    QuestionsModule
  ],
  providers: [
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    MsalService,

    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
