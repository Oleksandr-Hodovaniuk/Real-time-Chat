import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { UserModel } from '../models/user.model';
import { UserRegisterModel } from '../models/user.register.model';
import { UserLoginModel } from '../models/user.login';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + 'auth/';
  private storageKey = environment.storageKey;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  getUserFromLocalStorage(): UserModel | null {
    const user = localStorage.getItem(this.storageKey);
    return user ? JSON.parse(user) : null;
  }
  
  logout() {
    localStorage.removeItem(this.storageKey);
    this.router.navigate(['/login']);
  }

  getUser(data: string): Observable<UserModel> {
    return this.http.get<UserModel>(`${this.apiUrl}${data}`);
  }

  register(data: UserRegisterModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}register`, data);
  }

  login(data: UserLoginModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}login`, data);
  }
}