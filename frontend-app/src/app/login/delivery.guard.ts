import { CanActivateFn } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { inject } from '@angular/core';
import { UserType } from '../models/user.model';

export const deliveryGuard: CanActivateFn = (route, state) => {

    const authSvc = inject(AuthenticationService);
    if(authSvc.getUserRole()==UserType.ARTISAN ||authSvc.getUserRole()==UserType.DELIVERY_PARTNER) return true;
    alert("not authorized")
    return false;
};
