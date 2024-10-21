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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {LayoutModule} from "./features/layout/layout.module";
import { MatOptionModule } from '@angular/material/core';
import { RouterOutlet } from '@angular/router';
import { MatRadioModule } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QuestionsModule } from './features/questions/questions.module';

@NgModule({
  declarations: [
    AppComponent,
    CreateQuestionComponent,
  ],
  imports: [
    BrowserAnimationsModule,
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
    MatListModule,
    MatCheckboxModule,
    QuestionsModule,
    FormsModule,
    MatMenuModule,
    MatIconModule,
  ],
  providers: [
    provideAnimationsAsync(), HttpClientModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }