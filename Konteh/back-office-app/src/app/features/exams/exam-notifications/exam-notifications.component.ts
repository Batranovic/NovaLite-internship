import { Component, OnInit } from '@angular/core';
import {NotificationsService} from '../../../shared/notifications.service';

@Component({
  selector: 'app-exam-notifications',
  templateUrl: './exam-notifications.component.html',
})
export class ExamNotificationsComponent implements OnInit {
  receivedMessage: string = "test";

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
    this.notificationsService.messageReceived.subscribe((message) => {
      this.receivedMessage = message;
    });
  }
}
