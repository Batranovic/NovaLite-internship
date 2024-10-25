import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamOverviewComponent } from './exam-overview/exam-overview.component';
import { MatTableModule } from '@angular/material/table';
import { ExamStatusPipe } from './exam-status.pipe';
import { ExamNotificationsComponent } from './exam-notifications/exam-notifications.component';
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    ExamOverviewComponent,
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
