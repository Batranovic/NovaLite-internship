import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import { QuestionOverviewComponent } from './question-overview/question-overview.component';



@NgModule({
  declarations: [
    QuestionsOverviewComponent,
    QuestionOverviewComponent
  ],
  imports: [
    CommonModule
  ]
})
export class QuestionsModule { }
