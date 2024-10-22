import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import { QuestionOverviewComponent } from './question-overview/question-overview.component';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DeleteAnswerDialogComponent } from './delete-answer-dialog/delete-answer-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import {  MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { AnswerFormComponent } from './create-question/answer-form/answer-form.component';
import { CreateQuestionComponent } from './create-question/create-question.component';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { NgArrayPipesModule } from 'ngx-pipes';

@NgModule({
  declarations: [
    CreateQuestionComponent,
    QuestionsOverviewComponent,
    QuestionOverviewComponent,
    DeleteAnswerDialogComponent,
    AnswerFormComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatListModule,
    MatIconModule,
    MatMenuModule,
    MatRadioModule,
    BrowserAnimationsModule,
    FormsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatButtonModule,
    MatError,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatCheckboxModule,
    MatIconModule,
    NgArrayPipesModule
  ]
})
export class QuestionsModule { }