import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css',
})
export class NavBarComponent implements OnInit{

  constructor(private router: Router, private msalService: MsalService) {}

  ngOnInit(): void {
    this.msalService.instance.initialize().then(() => {
      this.msalService.instance.handleRedirectPromise().then(
        res => {
          if (res != null && res.account != null) {
            this.msalService.instance.setActiveAccount(res.account);
          }
        }
      )
    })
  }

  redirectToHome() {
    this.router.navigate(['']);
  }

  redirectToQuestionsOverview() {
    this.router.navigate(["questions-overview"]);
  }

  isLoggedIn(): boolean {
    return this.msalService.instance.getActiveAccount() != null;
  }

  login() {
    this.msalService.loginRedirect();
  }

  logout() {
    this.msalService.logout();
  }

  getAccountUsername(): string | null {
    const account = this.msalService.instance.getActiveAccount();
    return account ? account.username : null;
  }

  redirectToExamOverview() {
    this.router.navigate(["exam-notifications"]);

  }
}
