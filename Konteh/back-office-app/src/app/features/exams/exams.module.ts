import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamOverviewComponent } from './exam-overview/exam-overview.component';
import { MatTableModule } from '@angular/material/table';
import { ExamStatusPipe } from './exam-status.pipe';

@NgModule({
  declarations: [
    ExamOverviewComponent,
    ExamStatusPipe
  ],
  imports: [
    CommonModule,
    MatTableModule,
  ]
})
export class ExamsModule { }
