import { LoginUser } from './../../models/login.user';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { ChangePasswordDialogComponent } from '../change-password/change-password-dialog/change-password-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  @ViewChild(ChangePasswordComponent) changePasswordComponent: ChangePasswordComponent;

  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.auth.logout();
  }

  login() {
    const val = this.form.value;

    if (val.email && val.password) {
      this.api.login(val.email, val.password).subscribe((loginUser: LoginUser) => {
        this.auth.saveLoginToken(loginUser);
        if (this.auth.passwordChangeRequired()) {
          this.showChangePassword();
        } else {
          this.router.navigateByUrl('/');
        }
      });
    }
  }

  showChangePassword() {
    this.changePasswordComponent.openDialog();
  }
}
