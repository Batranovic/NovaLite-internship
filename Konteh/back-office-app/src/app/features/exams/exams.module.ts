import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamNotificationsComponent } from './exam-notifications/exam-notifications.component';
import {FormsModule} from "@angular/forms";



@NgModule({
  declarations: [
    ExamNotificationsComponent
  ],
    imports: [
        CommonModule,
        FormsModule
    ]
})
export class ExamsModule { }
