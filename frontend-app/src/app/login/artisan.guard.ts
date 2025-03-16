import { CanActivateFn } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { inject } from '@angular/core';

export const artisanGuard: CanActivateFn = (route, state) => {

    const authSvc = inject(AuthenticationService);
    if(authSvc.getUserRole()==authSvc.ARTISAN_ROLE ||authSvc.getUserRole()==authSvc.CUSTOMER_ROLE) return true;
    alert("not authorized")
    return false;
};
