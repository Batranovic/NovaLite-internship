import {  RouterModule, Routes } from "@angular/router";
import { ExamsOverviewComponent } from "./exams-overview/exams-overview.component";
import { NgModule } from "@angular/core";
import { ExamStatisticsComponent } from "./exam-statistics/exam-statistics.component";

const routes: Routes = [
    {
        path: '',
        component: ExamsOverviewComponent
    },
    {
        path: 'statistics',
        component: ExamStatisticsComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ExamsRoutingModule { }