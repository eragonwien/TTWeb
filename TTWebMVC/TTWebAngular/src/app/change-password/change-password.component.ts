import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ChangePasswordDialogComponent } from './change-password-dialog/change-password-dialog.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit {
  constructor(private dialog: MatDialog, private auth: AuthService) {}

  ngOnInit(): void {}

  openDialog() {
    this.dialog.open(ChangePasswordDialogComponent, {
      width: '600px',
      disableClose: true,
    });
  }
}
