import { shareReplay } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../services/api.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private apiService: ApiService, private auth: AuthService) {}

  ngOnInit() {}

  ping() {
    this.apiService.ping().pipe(shareReplay(1)).subscribe(this.onPingSuccess, this.onPingError);
  }

  private onPingSuccess() {
    alert('OK');
  }

  private onPingError(err: HttpErrorResponse) {
    alert(err.message);
  }
}
