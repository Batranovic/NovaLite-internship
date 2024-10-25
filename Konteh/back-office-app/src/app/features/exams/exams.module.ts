import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { ExamStatusPipe } from './exam-status.pipe';
import { ExamNotificationsComponent } from './exam-notifications/exam-notifications.component';
import { FormsModule } from "@angular/forms";
import { ExamsOverviewComponent } from './exams-overview/exams-overview.component';

@NgModule({
  declarations: [
    ExamsOverviewComponent,
    ExamStatusPipe,
    ExamNotificationsComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule
  ]
})
export class ExamsModule { }
