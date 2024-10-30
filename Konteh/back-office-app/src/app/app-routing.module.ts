import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './authorization/auth.guard';
import { NotFoundComponent } from './features/layout/not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: "questions",
    pathMatch: 'full'
  },
  {
    path: 'questions',
    canActivate: [AuthGuard],
    loadChildren: () => import('./features/questions/questions.module').then(m => m.QuestionsModule),
  },
  {
    path: 'exams',
    canActivate: [AuthGuard],
    loadChildren: () => import('./features/exams/exams.module').then(m => m.ExamsModule)
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
