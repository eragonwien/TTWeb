import { AuthService } from 'src/app/services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUser } from 'src/models/appUser.model';
import { ApiService } from '../services/api.service';
import { HelperService } from '../services/helper.service';
import { FormService } from '../services/form.service';
import { faUser, faEnvelope, faKey } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
  providers: [HelperService],
})
export class UserProfileComponent implements OnInit {
  form: FormGroup;
  appUser: AppUser;
  errors: string[] = [];
  @ViewChild('userProfileForm', { static: true }) userProfileForm: NgForm;

  faUser = faUser;
  faEnvelope = faEnvelope;
  faKey = faKey;

  constructor(
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private api: ApiService,
    private formService: FormService
  ) {}

  ngOnInit(): void {
    this.loadAppUser();
    this.initializeForm();
  }

  save() {
    if (this.form.valid) {
      const editAppUser = new AppUser(this.form.value);
      this.api.saveAppUser(editAppUser).subscribe(
        (appUser: AppUser) => this.onSaveSuccess(appUser),
        (err) => this.onSaveError(err)
      );
    }
  }

  private onSaveSuccess(appUser: AppUser) {
    this.appUser = appUser;
    this.auth.saveAppUser(appUser);
  }

  private onSaveError(err: HttpErrorResponse) {
    this.errors = this.formService.getModelStateErrors(err.error);
  }

  private loadAppUser() {
    this.appUser = this.auth.AppUser;
  }

  private initializeForm() {
    this.form = this.formBuilder.group({
      id: [this.appUser.id, [Validators.required]],
      firstname: [this.appUser.firstname, [Validators.required]],
      lastname: [this.appUser.lastname, [Validators.required]],
      email: [this.appUser.email, [Validators.required, Validators.email]],
      facebookUser: [this.appUser.facebookUser, [Validators.email]],
      facebookPassword: [this.appUser.facebookPassword],
    });
  }

  displayError(field: string, includes: string[] = [], excludes: string[] = []) {
    return this.formService.displayError(this.form, this.userProfileForm, field, includes, excludes);
  }

  clearNotifications() {
    this.errors = [];
  }
}
