import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  menuActive = false;
  userDropdownActive = false;
  faUser = faUser;
  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit(): void {}

  logout() {
    this.auth.logout();
    this.router.navigate(['login']);
  }

  toggleNavbarMenu() {
    this.menuActive = !this.menuActive;
  }

  closeNavbarMenu() {
    this.menuActive = false;
  }

  public get authenticated(): boolean {
    return this.auth.Authenticated;
  }

  public get userGreeting(): string {
    if (this.auth.Authenticated && this.auth.AppUser) {
      const appUser = this.auth.AppUser;
      return `${appUser.firstname} ${appUser.lastname}`;
    }
    return 'User';
  }
}
