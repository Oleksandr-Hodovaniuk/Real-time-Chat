import { Routes } from '@angular/router';
import { RegistrationComponent } from './features/auth/pages/registration/registration.component';
import { LoginComponent } from './features/auth/pages/login/login.component';
import { ChatComponent } from './features/chat/pages/chat/chat.component';
import { AuthGuard } from './features/auth/components/auth-guard.component';

export const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'register', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: 'chat', component: ChatComponent, canActivate: [AuthGuard] }
];
