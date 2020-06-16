import { ScheduleJobDef } from './../../models/scheduleJobDef.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../services/api.service';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { FormService } from '../services/form.service';
import { faUser, faEnvelope, faKey } from '@fortawesome/free-solid-svg-icons';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-schedule-job-def-form',
  templateUrl: './schedule-job-def-form.component.html',
  styleUrls: ['./schedule-job-def-form.component.scss'],
})
export class ScheduleJobDefFormComponent implements OnInit {
  form: FormGroup;
  typeOptions: string[] = [];
  intervalTypeOptions: string[] = [];
  @ViewChild('scheduleJobDefForm', { static: true }) scheduleJobDefForm: NgForm;
  faUser = faUser;

  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    private auth: AuthService,
    private formService: FormService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm() {
    this.form = this.formBuilder.group({
      name: [null],
      appUserId: [this.auth.AppUser.id, [Validators.required]],
      type: ['', [Validators.required]],
      intervalType: ['', [Validators.required]],
      timeFrom: [null, [Validators.required]],
      timeTo: [null, [Validators.required]],
      active: [false, [Validators.required]],
    });
  }

  displayError(field: string, includes: string[] = [], excludes: string[] = []) {
    return this.formService.displayError(this.form, this.scheduleJobDefForm, field, includes, excludes);
  }

  save() {}
}
