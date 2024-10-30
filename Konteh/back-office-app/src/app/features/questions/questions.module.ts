import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DeleteAnswerDialogComponent } from './delete-answer-dialog/delete-answer-dialog.component';
import { AnswerFormComponent } from './create-question/answer-form/answer-form.component';
import { CreateQuestionComponent } from './create-question/create-question.component';
import { QuestionFilterComponent } from './question-filter/question-filter.component';
import { CategoryNamePipe } from './category-name.pipe';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { HttpClientModule } from '@angular/common/http';
import { FormErrorsComponent } from "../../shared/form-errors.component";
import { QuestionsRoutingModule } from './questions-routing.module';
import { MaterialModule } from '../../shared/material/material.module';

@NgModule({
  declarations: [
    CreateQuestionComponent,
    QuestionsOverviewComponent,
    DeleteAnswerDialogComponent,
    AnswerFormComponent,
    QuestionFilterComponent,
    QuestionsOverviewComponent,
    CategoryNamePipe
  ],
  exports: [
    QuestionFilterComponent,
    QuestionsOverviewComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    FormErrorsComponent,
    QuestionsRoutingModule
]
})
export class QuestionsModule { }