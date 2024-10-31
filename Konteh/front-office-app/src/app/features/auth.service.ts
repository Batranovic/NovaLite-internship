import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private emailClaim: string | null = null;

  setEmailClaim(email: string) {
    this.emailClaim = email;
  }

  getEmailClaim(): string | null {
    return this.emailClaim;
  }

  clearEmailClaim() {
    this.emailClaim = null;
  }
}
