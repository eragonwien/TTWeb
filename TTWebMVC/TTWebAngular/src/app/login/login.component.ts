import { LoginUser } from './../../models/login.user';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { ChangePasswordDialogComponent } from '../change-password/change-password-dialog/change-password-dialog.component';
import { FormService } from '../services/form.service';
import { LoginViewModel } from 'src/models/loginViewModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  @ViewChild(ChangePasswordComponent) changePasswordComponent: ChangePasswordComponent;
  @ViewChild('loginForm', { static: true }) loginForm: NgForm;

  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    private auth: AuthService,
    private formService: FormService,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.auth.logout();
  }

  login() {
    if (this.form.valid) {
      const loginModel = new LoginViewModel(this.form.value);
      this.api.login(loginModel.email, loginModel.password).subscribe((loginUser: LoginUser) => {
        this.auth.saveLoginToken(loginUser);
        this.router.navigateByUrl('/');
      });
    }
  }

  showChangePassword() {
    this.changePasswordComponent.openDialog();
  }

  displayError(field: string, includes: string[] = [], excludes: string[] = []) {
    return this.formService.displayError(this.form, this.loginForm, field, includes, excludes);
  }
}
