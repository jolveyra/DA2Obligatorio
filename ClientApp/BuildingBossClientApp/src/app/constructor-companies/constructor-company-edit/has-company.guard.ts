import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, map, of, switchMap, take } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { ConstructorCompanyAdministratorService } from './../../services/constructor-company-administrator.service';

export const hasCompanyGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const constCompAdminService = inject(ConstructorCompanyAdministratorService);
  const router = inject(Router);
  
  return authService.userLogged.pipe(
    take(1),
    switchMap(user => {
      if (user.role === 'ConstructorCompanyAdmin') {
        return constCompAdminService.fetchConstructorCompanyAdministrator().pipe(
          map(constCompAdmin => {
            if (constCompAdmin.constructorCompanyName !== '') {
              const constCompAdminId = constCompAdmin.constructorCompanyId;
              return true;
            } else {
              return router.createUrlTree(['/constructorCompanies']);
            }
          }), catchError(() => {
            return of(router.createUrlTree(['/home']));
          }
        ));
      } else {
        return of(router.createUrlTree(['/home']));
      }
    })
  );
};