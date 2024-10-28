import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExamRegistrationComponent } from './features/exams/exam-registration/exam-registration.component';
import { ExamOverviewComponent } from './features/exams/exam-overview/exam-overview.component';

const routes: Routes = [
  {path: '', component: ExamRegistrationComponent},
  {path: 'exam-overview/:id', component: ExamOverviewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
