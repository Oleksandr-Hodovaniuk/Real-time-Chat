import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { UserModel } from '../models/user.model';
import { UserRegisterModel } from '../models/user.register.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + 'auth/';

  constructor(private http: HttpClient) {}

  register(data: UserRegisterModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}register`, data);
  }

  logout() {
    localStorage.removeItem('token');
  }
}