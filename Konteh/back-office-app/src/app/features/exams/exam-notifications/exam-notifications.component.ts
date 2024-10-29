import { Component, OnInit } from '@angular/core';
import {NotificationsService} from '../../../shared/notifications.service';
import {GetAllExamsResponse} from '../../../api/api-reference';

@Component({
  selector: 'app-exam-notifications',
  templateUrl: './exam-notifications.component.html',
})
export class ExamNotificationsComponent implements OnInit {
  receivedMessage: GetAllExamsResponse | null = null;

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
    this.notificationsService.messageReceived.subscribe((message) => {
      this.receivedMessage = message;
      console.log(this.receivedMessage);
    });
  }

  protected readonly toString = toString;
}
