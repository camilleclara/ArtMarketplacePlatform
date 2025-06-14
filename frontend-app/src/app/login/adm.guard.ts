import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthenticationService } from './authentication.service';

export const admGuard: CanActivateFn = (route, state) => {
  const authSvc = inject(AuthenticationService);
  if(authSvc.getUserRole()=="ADMIN") return true;
  alert("not authorized")
  return false;
};
