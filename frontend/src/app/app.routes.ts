import { Routes } from '@angular/router';
import { RegistrationComponent } from './features/auth/pages/registration/registration.component/registration.component';

export const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'register', component: RegistrationComponent }
];
