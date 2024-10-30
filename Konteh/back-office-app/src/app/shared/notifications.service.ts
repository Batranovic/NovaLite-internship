import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpTransportType } from '@microsoft/signalr';
import { environment } from '../environments/environment';
import { GetExamResponse } from '../api/api-reference';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private hubConnection: signalR.HubConnection;
  public messageReceived: EventEmitter<GetExamResponse> = new EventEmitter<GetExamResponse>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.hubUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      }).build();
    this.startConnection();
  }

  private startConnection(): void {
    this.hubConnection.start()
      .then(() => {
        this.setupListeners();
      });
  }

  private setupListeners(): void {
    this.hubConnection.on('ExamStartedOrSubmitted', (message: GetExamResponse) => {
      this.messageReceived.emit(message);
    });
  }
}

