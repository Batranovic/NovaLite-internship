import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuestionsOverviewComponent } from './features/questions/questions-overview/questions-overview.component';
import { HomeComponent } from './features/home/home.component';
import { CreateQuestionComponent } from './features/questions/create-question/create-question.component';
import { QuestionOverviewComponent } from './features/questions/question-overview/question-overview.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'questions-overview', component: QuestionsOverviewComponent },
  { path: 'create-question', component: CreateQuestionComponent },
  { path: 'question-overview/:id', component: QuestionOverviewComponent},
  { path: 'edit-question/:id', component: CreateQuestionComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
