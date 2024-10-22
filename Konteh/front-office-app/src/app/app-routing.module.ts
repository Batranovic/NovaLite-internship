import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExamOverviewComponent } from './features/exams/exam-overview/exam-overview.component';

const routes: Routes = [
  {path: '', component: ExamOverviewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
