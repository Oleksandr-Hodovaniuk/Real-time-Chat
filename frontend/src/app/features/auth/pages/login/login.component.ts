import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { UserRegisterModel } from '../../models/user.register.model';
import { AuthService } from '../../services/auth.service';
import { ModalService } from '../../../../shared/services/modal.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private modalService: ModalService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', [
        Validators.required,
        Validators.minLength(2), 
        Validators.maxLength(40),
        Validators.pattern("^[A-Za-z0-9' -]+$"),
      ]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(40),
        Validators.pattern("^[A-Za-z0-9' -]+$")
      ]],
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const user: UserRegisterModel = this.loginForm.value;

    this.authService.login(user).subscribe({
      next: (res) => {
        localStorage.setItem('user', JSON.stringify(res));
        this.router.navigate(['/chat']);
      },
      error: (err) => {     
       if (err.status === 404 || err.status === 400) {
          this.modalService.open('Login Error', err.error.error || 'Sorry, something went wrong!');
        }
        else {
          this.modalService.open('Login Error', 'Sorry, something went wrong!');
        }
      }
    });
  }
} 