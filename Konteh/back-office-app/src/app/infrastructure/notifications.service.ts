import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpTransportType } from '@microsoft/signalr';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private hubConnection: signalR.HubConnection;
  public messageReceived: EventEmitter<string> = new EventEmitter<string>();

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
      })
      .catch();
  }

  private setupListeners(): void {
    this.hubConnection.on('ReceiveMessage', (message: string) => {
      this.messageReceived.emit(message);
    });
  }
}
