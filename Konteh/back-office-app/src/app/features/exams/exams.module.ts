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

@NgModule({
  declarations: [
    ExamsOverviewComponent,
    ExamStatusPipe,
    ExamsSearchComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatFormFieldModule,
    MatIcon,
    ReactiveFormsModule,
    MatLabel,
    MatInputModule
  ]
})
export class ExamsModule { }
