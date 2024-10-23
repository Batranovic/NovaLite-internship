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

  constructor(private signalRService: NotificationsService) {}

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        this.receivedMessage = message;
        console.log(message);
      });
    });
  }

  sendMessage(message: string): void {
    this.signalRService.sendMessage(message);
  }
}
