import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TTWebAngular';
  screenWidth: number;
  isMobile: boolean;

  constructor() {
    // set screenWidth on page load
    this.screenWidth = window.innerWidth;
    this.isMobile = this.screenWidth <= 840;
    window.onresize = () => {
      // set screenWidth on screen size change
      this.screenWidth = window.innerWidth;
      this.isMobile = this.screenWidth <= 840;
    };
  }
}
