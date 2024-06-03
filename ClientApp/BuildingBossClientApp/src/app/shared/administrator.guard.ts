import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { map, take } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const administratorGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.userLogged.pipe(
    take(1),
    map(user => {
      if (user.role === 'Administrator') {
        return true;
      }
      return router.createUrlTree(['/home']);
    })
  );
};
