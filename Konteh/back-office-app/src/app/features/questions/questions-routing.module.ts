import { RouterModule, Routes } from "@angular/router";
import { QuestionsOverviewComponent } from "./questions-overview/questions-overview.component";
import { CreateQuestionComponent } from "./create-question/create-question.component";
import { NgModule } from "@angular/core";
import { QuestionStatisticsComponent } from "./question-statistics/question-statistics.component";

const routes: Routes  = [
    {
        path: '',
        component: QuestionsOverviewComponent
    },
    {
        path: 'create',
        component: CreateQuestionComponent
    },
    {
        path: 'edit/:id',
        component: CreateQuestionComponent
    },
    {
        path: 'statistics/:id',
        component: QuestionStatisticsComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class QuestionsRoutingModule { }