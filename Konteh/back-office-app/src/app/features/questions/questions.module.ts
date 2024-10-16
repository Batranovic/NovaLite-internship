import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsOverviewComponent } from './questions-overview/questions-overview.component';
import {
  MatCell, MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow, MatHeaderRowDef,
  MatRow, MatRowDef,
  MatTable
} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {HttpClientModule} from '@angular/common/http';
import { QuestionFilterComponent } from './question-filter/question-filter.component';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import {MatOption} from '@angular/material/core';
import {MatSelect} from '@angular/material/select';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatButton, MatButtonModule} from '@angular/material/button';
import { CategoryNamePipe } from './category-name.pipe';
import {MatMenu, MatMenuItem, MatMenuModule, MatMenuTrigger} from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';



@NgModule({
  declarations: [
    QuestionFilterComponent,
    QuestionsOverviewComponent,
    CategoryNamePipe
  ],
  exports:[
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    MatTable,
    MatColumnDef,
    MatHeaderCell,
    MatCell,
    MatHeaderRow,
    MatRow,
    MatPaginator,
    MatFormField,
    MatInput,
    MatLabel,
    MatOption,
    MatSelect,
    ReactiveFormsModule,
    MatHeaderCellDef,
    MatCellDef,
    MatButton,
    FormsModule,
    MatRowDef,
    MatHeaderRowDef,
    MatMenuTrigger,
    MatMenu,
    MatMenuItem,
    MatButtonModule,
    MatMenuModule,
    MatIconModule
  ]
})
export class QuestionsModule { }
