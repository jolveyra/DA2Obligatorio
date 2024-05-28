import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { AdministratorsComponent } from './administrators/administrators.component';
import { AdministratorListComponent } from './administrators/administrator-list/administrator-list.component';
import { AdministratorNewComponent } from './administrators/administrator-new/administrator-new.component';
import { administratorGuard } from './shared/administrator.guard';
import { authGuard } from './auth/auth.guard';
import { InvitationsComponent } from './invitations/invitations.component';
import { InvitationEditComponent } from './invitations/invitation-edit/invitation-edit.component';
import { InvitationListComponent } from './invitations/invitation-list/invitation-list.component';
import { InvitationNewComponent } from './invitations/invitation-new/invitation-new.component';
import { BuildingsComponent } from './buildings/buildings.component';
import { BuildingListComponent } from './buildings/building-list/building-list.component';
import { BuildingNewComponent } from './buildings/building-new/building-new.component';
import { BuildingFlatsComponent } from './buildings/building-flats/building-flats.component';
import { BuildingEditComponent } from './buildings/building-edit/building-edit.component';
import { BuildingFlatEditComponent } from './buildings/building-flat-edit/building-flat-edit.component';
import { buildingGuard } from './buildings/building.guard';
import { managerGuard } from './shared/manager.guard';
import { constructorCompanyAdminGuard } from './shared/constructor-company-admin.guard';

const routes: Routes = [
  { path: '', component: AuthComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] }, 
  { path: 'administrators', component: AdministratorsComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: AdministratorListComponent },
    { path: 'new', component: AdministratorNewComponent }
  ] },
  { path: 'invitations', component: InvitationsComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: InvitationListComponent },
    { path: 'new', component: InvitationNewComponent },
    { path: ':id', component: InvitationEditComponent } // FIXME: not implemented, should be the one that anyone can accept or reject, but I think it should be in a different module maybe
  ] },
  { path: 'buildings', component: BuildingsComponent, canActivate: [authGuard], children: [
    { path: '', component: BuildingListComponent, canActivate: [buildingGuard] },
    { path: 'new', component: BuildingNewComponent, canActivate: [constructorCompanyAdminGuard]},
    { path: ':buildingId', component: BuildingFlatsComponent, canActivate: [buildingGuard] },
    { path: ':buildingId/edit', component: BuildingEditComponent, canActivate: [managerGuard] },
    { path: ':buildingId/flats/:flatId', component: BuildingFlatEditComponent, canActivate: [managerGuard] }
  ] },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
