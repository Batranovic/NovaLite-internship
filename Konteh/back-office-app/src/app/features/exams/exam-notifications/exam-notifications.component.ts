import { Component } from '@angular/core';
import * as signalR from '@microsoft/signalr'
import {Observable} from 'rxjs';
import {NotificationsService} from '../../../infrastructure/notifications.service';

@Component({
  selector: 'app-exam-notifications',
  templateUrl: './exam-notifications.component.html',
})
export class ExamNotificationsComponent {
  receivedMessage: string = "test";

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
    this.notificationsService.startConnection().subscribe(() => {
      this.notificationsService.receiveMessage().subscribe((message) => {
        this.receivedMessage = message;
      });
    });
  }
}
