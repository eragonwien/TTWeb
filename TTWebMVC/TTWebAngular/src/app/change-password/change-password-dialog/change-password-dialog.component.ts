import { ChangePasswordModel } from '../../../models/changePassword.model';
import { AppUser } from 'src/models/appUser.model';
import { MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { FormService } from 'src/app/services/form.service';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss'],
})
export class ChangePasswordDialogComponent implements OnInit {
  form: FormGroup;
  appUser: AppUser;
  @ViewChild('changePasswordForm', { static: true }) changePasswordForm: NgForm;
  constructor(
    public dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private formService: FormService,
    private api: ApiService
  ) {
    this.appUser = this.auth.AppUser;
    this.initiateForm();
  }

  ngOnInit() {}

  submit() {
    if (this.form.valid) {
    }
  }

  cancel() {
    this.auth.logout();
    this.dialogRef.close();
  }

  private initiateForm() {
    this.form = this.formBuilder.group(
      {
        id: [this.appUser.id, Validators.required],
        'old-password': ['', Validators.required],
        'new-password': ['', Validators.required],
        'new-password-confirm': ['', Validators.required],
      },
      { validators: this.validateChangePassword }
    );
  }

  private validateChangePassword(formGroup: FormGroup) {
    const newPassword = formGroup.get('new-password').value;
    const confirmPassword = formGroup.get('new-password-confirm').value;

    if (newPassword !== confirmPassword) {
      formGroup.controls['new-password-confirm'].setErrors({ notMatch: true });
    }
  }

  displayError(field: string, includes: string[] = [], excludes: string[] = []) {
    return this.formService.displayError(this.form, this.changePasswordForm, field, includes, excludes);
  }
}
