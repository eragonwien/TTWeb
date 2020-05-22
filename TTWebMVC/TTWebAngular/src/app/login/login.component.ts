import { shareReplay } from 'rxjs/operators';
import { AppUser } from '../../models/appUser.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { FormService } from '../services/form.service';
import { LoginViewModel } from 'src/models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup;
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
      this.api
        .login(loginModel.email, loginModel.password)
        .pipe(shareReplay(1))
        .subscribe((appUser: AppUser) => {
          this.auth.saveAppUser(appUser);
          this.router.navigateByUrl('/');
        });
    }
  }

  displayError(field: string, includes: string[] = [], excludes: string[] = []) {
    return this.formService.displayError(this.form, this.loginForm, field, includes, excludes);
  }
}
