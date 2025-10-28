import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ModalComponent } from "./shared/components/modal.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ModalComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected title = 'frontend';
}
