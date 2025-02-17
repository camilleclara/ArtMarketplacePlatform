import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const jwt = sessionStorage.getItem("jwt");
  //naive because should check the expiration

  if(jwt) {
    return true;
  }

  router.navigate(["login"]);
  return false;
};
