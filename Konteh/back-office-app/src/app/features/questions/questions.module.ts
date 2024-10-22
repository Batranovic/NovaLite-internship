import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import { QuestionFilterComponent } from './question-filter/question-filter.component';
import { CategoryNamePipe } from './category-name.pipe';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
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
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    MatSnackBarModule
  ]
})
export class QuestionsModule { }
