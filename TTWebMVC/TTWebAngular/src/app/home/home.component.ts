import { RoutingParamsType } from './../../models/routingParams.model';
import { SettingService } from './../services/setting.service';
import { ScheduleJob } from './../../models/scheduleListEntry.model';
import { shareReplay } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../services/api.service';
import { AuthService } from '../services/auth.service';
import { faEdit, faTrash, faPlus } from '@fortawesome/free-solid-svg-icons';
import { ScheduleJobDetail } from 'src/models/scheduleListEntry.model';
import { RoutingParams } from 'src/models/routingParams.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  faEdit = faEdit;
  faTrash = faTrash;
  faPlus = faPlus;
  jobs: ScheduleJobDetail[] = [];
  constructor(private apiService: ApiService, private auth: AuthService, private setting: SettingService) {}

  ngOnInit() {
    this.loadScheduleList();
  }

  ping() {
    this.apiService.ping().pipe(shareReplay(1)).subscribe(this.onPingSuccess, this.onPingError);
  }

  private onPingSuccess() {
    alert('OK');
  }

  private onPingError(err: HttpErrorResponse) {
    alert(err.message);
  }

  loadScheduleList() {
    this.apiService
      .getScheduleList()
      .pipe(shareReplay(1))
      .subscribe(this.onLoadScheduleListSuccess, this.onLoadScheduleListError);
  }

  onLoadScheduleListSuccess(jobs: ScheduleJob[]) {
    console.log(jobs);
  }

  onLoadScheduleListError(err: HttpErrorResponse) {
    alert(err.message);
  }

  public get scheduleJobDefFormCreateParam(): RoutingParams {
    return new RoutingParams(RoutingParamsType.create);
  }
}
