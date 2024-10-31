import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { ExamStatusPipe } from './exam-status.pipe';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ExamsOverviewComponent } from './exams-overview/exams-overview.component';
import { ExamsSearchComponent } from './exams-search/exams-search.component';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ExamsRoutingModule } from './exams-routing.module';
import { ExamStatisticsComponent } from './exam-statistics/exam-statistics.component';

@NgModule({
  declarations: [
    ExamsOverviewComponent,
    ExamStatusPipe,
    ExamsSearchComponent,
    ExamStatisticsComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatFormFieldModule,
    MatIcon,
    ReactiveFormsModule,
    MatLabel,
    MatInputModule,
    ExamsRoutingModule
  ]
})
export class ExamsModule { }
