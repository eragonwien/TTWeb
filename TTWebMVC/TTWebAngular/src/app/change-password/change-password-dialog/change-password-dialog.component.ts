import { MatDialogRef } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss'],
})
export class ChangePasswordDialogComponent implements OnInit {
  form: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.initiateForm();
  }

  ngOnInit() {}

  submit() {}

  cancel() {
    this.auth.logout();
    this.dialogRef.close();
  }

  private initiateForm() {
    this.form = this.formBuilder.group(
      {
        email: ['', Validators.required],
        'new-password': ['', Validators.required],
        'new-password-confirm': ['', Validators.required],
      },
      { validators: this.validateChangePassword }
    );
  }

  private validateChangePassword(form: FormGroup) {
    const newPassword = form.get('new-password');
    const confirmPassword = form.get('new-password-confirm');

    return newPassword === confirmPassword ? null : { notSame: true };
  }
}
