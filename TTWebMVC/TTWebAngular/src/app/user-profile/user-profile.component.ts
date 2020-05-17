import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';
import { Component, OnInit, enableProdMode, ViewChild } from '@angular/core';
import { AppUser } from 'src/models/appUser.model';
import { ApiService } from '../services/api.service';
import { AuthService } from '../services/auth.service';
import { HelperService } from '../services/helper.service';
import { FormService } from '../services/form.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
  providers: [HelperService],
})
export class UserProfileComponent implements OnInit {
  form: FormGroup;
  appUser: AppUser;
  @ViewChild('userProfileForm', { static: true }) userProfileForm: NgForm;

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
      this.api.saveAppUser(this.appUser).subscribe(this.onSaveSuccess);
    }
  }

  private onSaveSuccess(appUser: AppUser) {
    this.appUser = appUser;
    this.auth.saveAppUser(appUser);
  }

  private loadAppUser() {
    this.appUser = this.auth.AppUser;
  }

  private initializeForm() {
    this.form = this.formBuilder.group({
      title: [this.appUser.title, [Validators.required]],
      firstname: [this.appUser.firstname, [Validators.required]],
      lastname: [this.appUser.lastname, [Validators.required]],
      email: [this.appUser.email, [Validators.required, Validators.email]],
    });
  }

  displayError(field: string, includes: string[] = [], excludes: string[] = []) {
    return this.formService.displayError(this.form, this.userProfileForm, field, includes, excludes);
  }
}
