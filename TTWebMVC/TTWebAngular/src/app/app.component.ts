import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'TTWebAngular';
  screenWidth: number;
  isMobile: boolean;
  forceHidden: boolean;

  constructor(private auth: AuthService, private router: Router) {
    this.setScreenWidth();
    this.setScreenWidthOnSizeChange();

    router.events.subscribe((r) => this.onRouteChange(r));
  }

  setScreenWidth() {
    this.screenWidth = window.innerWidth;
    this.isMobile = this.screenWidth <= 840;
  }

  setScreenWidthOnSizeChange() {
    window.onresize = () => {
      this.screenWidth = window.innerWidth;
      this.isMobile = this.screenWidth <= 840;
    };
  }

  onRouteChange(event) {
    if (event instanceof NavigationStart) {
    } else if (event instanceof NavigationEnd) {
      this.forceHidden = event.url === '/login';
    }
  }
}
