import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { UserRegisterModel } from '../../models/user.register.model';
import { AuthService } from '../../services/auth.service';
import { ModalService } from '../../../../shared/services/modal.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './registration.component.html',
})
export class RegistrationComponent {
  registerForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private modalService: ModalService,
    private router: Router)
  {}

  ngOnInit() {
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

    const user: UserRegisterModel = this.registerForm.value;

    this.authService.register(user).subscribe({
      next: (res) => {
        localStorage.setItem('user', JSON.stringify(res));
        this.router.navigate(['/chat']);
      },
      error: (err) => {
        this.modalService.open('Registration Error', err.error.error || 'Sorry, something went wrong!');
      }
    });
  }
} 