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
  result: string;
  constructor(private apiService: ApiService, private auth: AuthService) {}

  ngOnInit() {}

  ping() {
    this.apiService.ping().subscribe(
      () => {
        this.result = 'OK';
      },
      (err: HttpErrorResponse) => {
        this.result = err.message;
      }
    );
  }
}
