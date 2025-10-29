import { AfterViewChecked, Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule, AsyncPipe } from '@angular/common';
import { ChatService } from '../../services/chat.service';
import { delay, Observable } from 'rxjs';
import { MessageModel } from '../../models/message.model';
import { AuthService } from '../../../auth/services/auth.service';
import { UserModel } from '../../../auth/models/user.model';
import { HeaderComponent } from "../../components/header.component";

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, AsyncPipe, HeaderComponent],
  templateUrl: './chat.component.html'
})
export class ChatComponent implements AfterViewChecked{
  messages$!: Observable<MessageModel[]>;
  user: UserModel | null = null;

  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;

  constructor(
    private chatService: ChatService,
    private authService: AuthService
  ) 
  {}

  ngOnInit() {
    this.messages$ = this.chatService.getAllMessages();
    this.user = this.authService.getUser();
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  private scrollToBottom(): void {
    if (this.messagesContainer) {
      this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
    }
  }
}