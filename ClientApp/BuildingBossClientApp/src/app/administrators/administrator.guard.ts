import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { inject } from '@angular/core';
import { map, take } from 'rxjs';

export const administratorGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.userLogged.pipe(
    take(1),
    map(user => {
      if (user.token !== '' && user.role === 'Administrator') {
        return true;
      }
      return router.createUrlTree(['/home']);
    })
  );
};
