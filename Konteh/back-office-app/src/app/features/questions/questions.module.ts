import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import { QuestionTableItemComponent } from './question-table-item/question-table-item.component';
import {MatCell, MatColumnDef, MatHeaderCell, MatHeaderRow, MatRow, MatTable} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';



@NgModule({
  declarations: [
    QuestionsOverviewComponent,
    QuestionTableItemComponent
  ],
  imports: [
    CommonModule,
    MatTable,
    MatColumnDef,
    MatHeaderCell,
    MatCell,
    MatHeaderRow,
    MatRow,
    MatPaginator
  ]
})
export class QuestionsModule { }
