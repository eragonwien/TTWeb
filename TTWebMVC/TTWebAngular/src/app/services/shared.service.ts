import { NotificationAlert, NotificationAlertType } from './../../models/notification.model';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class SharedService {
  private notification = new BehaviorSubject(null);
  sharedNotification = this.notification.asObservable();

  constructor() {}

  addNotification(n: NotificationAlert) {
    this.notification.next(n);
  }
}
