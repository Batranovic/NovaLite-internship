import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private msalService: MsalService) {}

  canActivate(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot){
    return this.msalService.instance.getActiveAccount() != null;
  }
}
