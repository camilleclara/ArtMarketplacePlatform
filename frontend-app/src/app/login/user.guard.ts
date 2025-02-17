import { CanActivateFn } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { inject } from '@angular/core';

export const userGuard: CanActivateFn = (route, state) => {
    const authSvc = inject(AuthenticationService);
    if(authSvc.getUserRoles()=="USER") return true;
    alert("not authorized")
    return false;
};
