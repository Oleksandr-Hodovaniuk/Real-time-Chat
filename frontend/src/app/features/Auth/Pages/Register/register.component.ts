import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserRegister } from './Models/user.register.model';
import { AuthService } from './Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder,
     private authService: AuthService,
     private router: Router)
  {
    this.registerForm = this.fb.group({
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
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const user: UserRegister = this.registerForm.value;

    this.authService.register(user).subscribe({
      next: (res) => {
        localStorage.setItem('user', JSON.stringify(res));
      },
      error: (err) => {
        alert("Sorry, something went wrong!");
      }
    });
  }
} 