import { Component } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css',


})
export class NavBarComponent {
  constructor(private router: Router) {
  }

  redirectToHome() {
    this.router.navigate(['']).then(r => {});
  }

  redirectToQuestionsOverview() {
    this.router.navigate(["questions-overview"]).then(r => {});
  }
}
