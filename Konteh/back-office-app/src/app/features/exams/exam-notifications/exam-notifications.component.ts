import { Component, OnInit } from '@angular/core';
import { NotificationsService } from '../../../infrastructure/notifications.service';

@Component({
  selector: 'app-exam-notifications',
  templateUrl: './exam-notifications.component.html',
})
export class ExamNotificationsComponent implements OnInit {
  receivedMessage: string = "test";

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
    this.notificationsService.startConnection()
      .then(() => {
        this.notificationsService.messageReceived.subscribe((message) => {
          this.receivedMessage = message;
        });
      });
  }
}
