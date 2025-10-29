import { Component } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  user: UserModel | null = null;
  
  constructor(public authService: AuthService) {}

  ngOnInit() {
    this.user = this.authService.getUserFromLocalStorage();
  }
}
