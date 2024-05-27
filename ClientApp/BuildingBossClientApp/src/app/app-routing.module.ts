import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { AdministratorsComponent } from './administrators/administrators.component';
import { AdministratorListComponent } from './administrators/administrator-list/administrator-list.component';
import { AdministratorNewComponent } from './administrators/administrator-new/administrator-new.component';
import { administratorGuard } from './administrators/administrator.guard';

const routes: Routes = [
  { path: '', component: AuthComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent }, //FIXME: User guards to protect this route
  { path: 'administrators', component: AdministratorsComponent, canActivate: [administratorGuard], children: [ //FIXME: User guards to protect this route from auth and role
    { path: '', component: AdministratorListComponent },
    { path: 'new', component: AdministratorNewComponent }
  ] },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
