import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-model-state-error-notification',
  templateUrl: './model-state-error-notification.component.html',
  styleUrls: ['./model-state-error-notification.component.scss'],
})
export class ModelStateErrorNotificationComponent implements OnInit, OnChanges {
  @Input() error: HttpErrorResponse;
  @Input() successNotification: string;
  errors: string[] = [];
  constructor(private formService: FormService) {}

  ngOnInit(): void {}

  clearSuccessNotifications() {
    this.successNotification = null;
  }

  clearErrorNotifications() {
    this.errors = [];
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.error) {
      this.handleErrorChanged(changes.error.currentValue);
    }
  }

  private handleErrorChanged(err: HttpErrorResponse) {
    this.errors = this.formService.getModelStateErrors(err);
  }

  get showErrors() {
    return this.error && this.errors && this.errors.length > 0;
  }

  get showSuccessNotification() {
    return this.successNotification && this.successNotification.length > 0;
  }
}
