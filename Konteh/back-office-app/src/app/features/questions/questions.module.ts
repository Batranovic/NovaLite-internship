import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import { QuestionOverviewComponent } from './question-overview/question-overview.component';
import { MatCardModule } from '@angular/material/card';
import { MatRadioButton, MatRadioGroup } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    QuestionsOverviewComponent,
    QuestionOverviewComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatRadioButton,
    MatRadioGroup,
    MatListModule,
    MatIconModule,
    MatMenuModule,
    BrowserAnimationsModule
  ]
})
export class QuestionsModule { }