import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map, take } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const buildingGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.userLogged.pipe(
    take(1),
    map(user => {
      if (user.role === 'Manager' || user.role === 'ConstructorCompanyAdmin') {
        return true;
      }
      return router.createUrlTree(['/home']);
    }
  ));
};