import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateQuestionComponent } from './features/questions/create-question/create-question.component';

const routes: Routes = [
  { path: 'create-question', component: CreateQuestionComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
