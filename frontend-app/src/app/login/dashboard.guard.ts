import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';



export const dashboardGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthenticationService);
  const role = authService.getUserRole();

  if (role === authService.CUSTOMER_ROLE) {
    router.navigate(['/dashboard-customer']);
    return false;
  } else if (role === authService.ARTISAN_ROLE) {
    router.navigate(['/dashboard-artisan']);
    return false;
  } else if (role === authService.ADMIN_ROLE) {
    router.navigate(['/dashboard-admin']);
    return false;
  }
  else if (role === authService.DELIVERY_ROLE) {
    router.navigate(['/dashboard-delivery']);
    return false;
  }
  router.navigate(['/login']); // Si aucun rôle, retour à la connexion
  return false;
};

