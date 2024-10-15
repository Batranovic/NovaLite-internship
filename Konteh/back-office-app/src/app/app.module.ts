import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CreateQuestionComponent } from './features/questions/create-question/create-question.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { NavBarComponent } from './features/layout/nav-bar/nav-bar.component';
import {LayoutModule} from "./features/layout/layout.module";
import { MatOptionModule } from '@angular/material/core';
import { RouterOutlet } from '@angular/router';
import { MatRadioModule } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';

@NgModule({
  declarations: [
    AppComponent,
    CreateQuestionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatButtonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterOutlet,
    MatRadioModule,
    MatListModule
  ],
  providers: [
    provideAnimationsAsync(), HttpClientModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
