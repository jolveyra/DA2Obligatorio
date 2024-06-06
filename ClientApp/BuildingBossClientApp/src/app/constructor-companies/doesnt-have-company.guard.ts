import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, map, of, switchMap, take } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { ConstructorCompanyAdministratorService } from './../services/constructor-company-administrator.service';

export const doesntHaveCompanyGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const constCompAdminService = inject(ConstructorCompanyAdministratorService);
  const router = inject(Router);

  // return authService.userLogged.pipe(
  //   take(1),
  //   map(user => {
  //     let empresa: boolean = false;
  //     let isError: boolean = false;
  //     let constructorCompanyAdmin = false;
  //     let constCompAdminId: string = '';
  //     if (user.role === 'ConstructorCompanyAdmin') {
  //       constructorCompanyAdmin = true;
  //       //FIXME: constCompAdminService.fetchConstructorCompanyAdministrator(user.id)
  //       return constCompAdminService.fetchConstructorCompanyAdministrator()
  //       .pipe(
  //         map(
  //         constCompAdmin => {
  //           if (constCompAdmin.constructorCompanyName !== '') {
  //             empresa = true;
  //             constCompAdminId = constCompAdmin.constructorCompanyId;
  //             return router.createUrlTree([`/constructorCompanies/${constCompAdminId}`]);
  //           } else {
  //             empresa = false;
  //             return true;
  //           }
  //         }))    
  //     } else {
  //       return router.createUrlTree(['/home']);
  //     };
      
  //       //FIXME: we may have to change this, because if the subscribe takes time, it may return the variables values before the subscribe is done
  //     // if (!constructorCompanyAdmin) {
  //     //   return router.createUrlTree(['/home']);
  //     // }

  //     // if (isError) {
  //     //   return router.createUrlTree(['/home']);
  //     // }

  //     // if(empresa) {
  //     //   return router.createUrlTree([`/constructorCompanies/${constCompAdminId}`]);
  //     // } else if(!empresa) {
  //     //   return true;
  //     // } else {
  //     //   return router.createUrlTree(['/home']);
  //     // }

  //   }));


    return authService.userLogged.pipe(
      take(1),
      switchMap(user => {
        if (user.role === 'ConstructorCompanyAdmin') {
          return constCompAdminService.fetchConstructorCompanyAdministrator().pipe(
            map(constCompAdmin => {
              if (constCompAdmin.constructorCompanyName !== '') {
                const constCompAdminId = constCompAdmin.constructorCompanyId;
                return router.createUrlTree([`/constructorCompanies/${constCompAdminId}`]);
              } else {
                return true;
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