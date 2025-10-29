import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { MessageModel } from '../models/message.model';



@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = environment.apiUrl + 'messages/';

  constructor(private http: HttpClient) {}

  getAllMessages(): Observable<MessageModel[]> {
      return this.http.get<MessageModel[]>(this.apiUrl);
  }
}