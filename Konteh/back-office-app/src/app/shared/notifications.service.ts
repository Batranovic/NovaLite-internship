import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpTransportType } from '@microsoft/signalr';
import { environment } from '../environments/environment';
import {GetAllExamsResponse} from '../api/api-reference';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private hubConnection: signalR.HubConnection;
  public messageReceived: EventEmitter<GetAllExamsResponse> = new EventEmitter<GetAllExamsResponse>();

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
    this.hubConnection.on('ReceiveMessage', (message: GetAllExamsResponse) => {
      this.messageReceived.emit(message);
    });
  }
}

