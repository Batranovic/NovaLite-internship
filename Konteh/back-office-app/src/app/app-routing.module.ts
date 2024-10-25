import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuestionsOverviewComponent } from './features/questions/questions-overview/questions-overview.component';
import { HomeComponent } from './features/home/home.component';
import { CreateQuestionComponent } from './features/questions/create-question/create-question.component';
import { AuthGuard } from './authorization/auth.guard';
import {ExamNotificationsComponent} from './features/exams/exam-notifications/exam-notifications.component';
import { ExamsOverviewComponent } from './features/exams/exams-overview/exams-overview.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'questions-overview', component: QuestionsOverviewComponent, canActivate:[AuthGuard]},
  { path: 'exam-notifications', component: ExamNotificationsComponent, canActivate:[AuthGuard]},
  { path: 'create-question', component: CreateQuestionComponent, canActivate: [AuthGuard] },
  { path: 'edit-question/:id', component: CreateQuestionComponent, canActivate: [AuthGuard] },
  { path: 'exams-overview', component: ExamsOverviewComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
