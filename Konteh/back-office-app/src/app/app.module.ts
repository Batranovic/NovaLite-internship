import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {LayoutModule} from "./features/layout/layout.module";
import {HttpClientModule} from '@angular/common/http';
import { QuestionsModule } from './features/questions/questions.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
    imports: [
        HttpClientModule,
        BrowserModule,
        AppRoutingModule,
        LayoutModule,
        QuestionsModule
    ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
