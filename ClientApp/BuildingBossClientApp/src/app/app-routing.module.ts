import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { AdministratorsComponent } from './administrators/administrators.component';
import { AdministratorListComponent } from './administrators/administrator-list/administrator-list.component';
import { AdministratorNewComponent } from './administrators/administrator-new/administrator-new.component';
import { administratorGuard } from './shared/administrator.guard';
import { authGuard } from './shared/auth.guard';
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
import { loggedGuard } from './auth/logged.guard';
import { RequestsComponent } from './requests/requests.component';
import { RequestListComponent } from './requests/request-list/request-list.component';
import { RequestNewComponent } from './requests/request-new/request-new.component';
import { RequestEditComponent } from './requests/request-edit/request-edit.component';
import { requestGuard } from './requests/request.guard';
import { MaintenanceEmployeesComponent } from './maintenance-employees/maintenance-employees.component';
import { EmployeeListComponent } from './maintenance-employees/employee-list/employee-list.component';
import { EmployeeNewComponent } from './maintenance-employees/employee-new/employee-new.component';
import { ReportsComponent } from './reports/reports.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { ConstructorCompaniesComponent } from './constructor-companies/constructor-companies.component';

const routes: Routes = [
  { path: '', component: AuthComponent, pathMatch: 'full', canActivate: [loggedGuard] },
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
  { path: 'constructorCompany', component: ConstructorCompaniesComponent, canActivate: [authGuard, constructorCompanyAdminGuard] },
  { path: 'buildings', component: BuildingsComponent, canActivate: [authGuard], children: [
    { path: '', component: BuildingListComponent, canActivate: [buildingGuard] },
    { path: 'new', component: BuildingNewComponent, canActivate: [constructorCompanyAdminGuard]},
    { path: ':buildingId', component: BuildingFlatsComponent, canActivate: [buildingGuard] }, // FIXME: not finished, have to check whether user is manager or admin
    { path: ':buildingId/edit', component: BuildingEditComponent, canActivate: [managerGuard] }, // FIXME: not finished, have to check whether managers are allowed to edit
    { path: ':buildingId/flats/:flatId', component: BuildingFlatEditComponent, canActivate: [managerGuard] }
  ] },
  { path: 'requests', component: RequestsComponent, canActivate: [authGuard], children: [
    { path: '', component: RequestListComponent, canActivate: [requestGuard] },
    { path: 'new', component: RequestNewComponent, canActivate: [managerGuard] },
    { path: ':id', component: RequestEditComponent, canActivate: [managerGuard] }
  ] },
  { path: 'maintenanceEmployees', component: MaintenanceEmployeesComponent, canActivate: [authGuard], children: [
    { path: '', component: EmployeeListComponent, canActivate: [managerGuard] },
    { path: 'new', component: EmployeeNewComponent, canActivate: [managerGuard] },
  ] },
  { path: 'reports', component: ReportsComponent, canActivate: [authGuard, managerGuard] },
  { path: 'userSettings', component: UserSettingsComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
