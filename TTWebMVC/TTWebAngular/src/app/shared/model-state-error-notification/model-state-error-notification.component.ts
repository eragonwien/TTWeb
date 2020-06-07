import { SharedService } from './../../services/shared.service';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormService } from 'src/app/services/form.service';
import { NotificationAlert, NotificationAlertType } from 'src/models/notification.model';

@Component({
  selector: 'app-model-state-error-notification',
  templateUrl: './model-state-error-notification.component.html',
  styleUrls: ['./model-state-error-notification.component.scss'],
})
export class ModelStateErrorNotificationComponent implements OnInit {
  notification: NotificationAlert;

  constructor(private formService: FormService, private sharedService: SharedService) {}

  ngOnInit(): void {
    this.listenForNotifications();
  }

  clearNotification() {
    this.notification = null;
  }

  listenForNotifications() {
    this.sharedService.sharedNotification.subscribe((n: NotificationAlert) => {
      this.clearNotification();
      this.addNotification(n);
    });
  }

  addNotification(n: NotificationAlert) {
    if (!n) {
      return;
    }
    this.notification = n;
  }

  get notificationClass() {
    return {
      'is-success': this.notification.type === NotificationAlertType.success,
      'is-danger': this.notification.type === NotificationAlertType.error,
    };
  }
}
