import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './features/auth/services/auth.service';
import { ModalComponent } from "./shared/components/modal.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrls: ['./app.scss'],
  imports: [RouterOutlet, ModalComponent]
})
export class App implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    const user = this.authService.getUserFromLocalStorage();
    if (user) {
      this.authService.getUser(user.id).subscribe({
        next: (res) => {
          if (res) {
            this.router.navigate(['/chat']);
          }
        },
        error: () => {
          this.router.navigate(['/login']);
        }
      });
    }
  }
}