import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ModalDataModel } from '../models/modal-data.model';

@Injectable({ providedIn: 'root' })
export class ModalService {
  private modalState = new BehaviorSubject<ModalDataModel | null>(null);
  modalState$ = this.modalState.asObservable();

  open(title: string, message: string) {
    this.modalState.next({ title, message });
  }

  close() {
    this.modalState.next(null);
  }
}