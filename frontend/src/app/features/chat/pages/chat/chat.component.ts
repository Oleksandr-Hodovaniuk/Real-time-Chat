import { AfterViewChecked, Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule, AsyncPipe } from '@angular/common';
import { ChatService } from '../../services/chat.service';
import { delay, Observable } from 'rxjs';
import { MessageModel } from '../../models/message.model';
import { AuthService } from '../../../auth/services/auth.service';
import { UserModel } from '../../../auth/models/user.model';
import { HeaderComponent } from "../../components/header.component";
import { ChatSignalRService } from '../../services/chat-signalr.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, AsyncPipe, HeaderComponent, FormsModule],
  templateUrl: './chat.component.html'
})
export class ChatComponent implements AfterViewChecked{
  messages$!: Observable<MessageModel[]>;
  user: UserModel | null = null;
  newMessage = '';

  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;


  constructor(
    private chatService: ChatService,
    private authService: AuthService,
    private chatSignalRService: ChatSignalRService
  ) 
  {}

  ngOnInit() {
    this.user = this.authService.getUserFromLocalStorage();
    this.messages$ = this.chatSignalRService.messages$;
    this.chatSignalRService.startConnection();
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  private scrollToBottom(): void {
    if (this.messagesContainer) {
      this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
    }
  }

  sendMessage() {
    if (this.newMessage.trim() === '') return;

    const message: MessageModel = {
      userId: this.user!.id,
      username: this.user!.username,
      text: this.newMessage,
      sentimentType: "",
      created: ""
    };

    this.chatSignalRService.sendMessage(message);
    this.newMessage = '';
  }

  onEnter(event: KeyboardEvent) {
    if (!event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  getEmoji(sentiment: string | undefined): string {
    switch (sentiment) {
      case 'Positive': return '😊';
      case 'Neutral': return '😐';
      case 'Negative': return '😡';
      default: return '🤔';
    }
  }
}