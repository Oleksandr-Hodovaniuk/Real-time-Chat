import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../../environments/environment';
import { UserRegister } from '../Models/user.register.model';
import { UserLogin } from '../Models/user.login.model';
import { UserDto } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + 'auth/';

  constructor(private http: HttpClient) {}

  register(data: UserRegister): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.apiUrl}register`, data);
  }

  login(data: UserLogin): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.apiUrl}login`, data);
  }

  logout() {
    localStorage.removeItem('token');
  }
}