import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { MessageModel } from '../models/message.model';
import { ChatService } from './chat.service';
import { ModalService } from '../../../shared/services/modal.service';

@Injectable({
  providedIn: 'root'
})
export class ChatSignalRService {
  private hubConnection!: signalR.HubConnection;
  private messagesSubject = new BehaviorSubject<MessageModel[]>([]);
  private chatHubUrl = environment.chatHubUrl;
  messages$ = this.messagesSubject.asObservable();

  constructor(
    private chatService: ChatService,
    private modalService: ModalService
  ) {}

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.chatHubUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connected'))
      .catch(err => console.error(err));

    this.chatService.getAllMessages().subscribe(msgs => {
      this.messagesSubject.next(msgs);
    });

    // New message received
    this.hubConnection.on('ReceiveMessage', (msg: MessageModel) => {
      const current = this.messagesSubject.value;
      this.messagesSubject.next([...current, msg]);
    });
  }

  async sendMessage(message: MessageModel) {
    if (this.hubConnection.state !== signalR.HubConnectionState.Connected) {
      console.warn('SignalR not connected yet, waiting...');
      await this.ensureConnection();
    }

    this.hubConnection.invoke('SendMessage', message)
      .catch(err => {
        this.modalService.open('Send message error', err || 'Sorry, something went wrong!');
      });
  }

private async ensureConnection(): Promise<void> {
    if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
      try {
        await this.hubConnection.start();
        console.log('SignalR connected (reconnect)');
      } catch (err) {
        console.error('Failed to connect to SignalR:', err);
      }
    }

    while (this.hubConnection.state !== signalR.HubConnectionState.Connected) {
      await new Promise(r => setTimeout(r, 200));
    }
  }
}