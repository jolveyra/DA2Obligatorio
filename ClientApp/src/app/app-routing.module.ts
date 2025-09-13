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
import { ManagersComponent } from './managers/managers.component';
import { ManagerListComponent } from './managers/manager-list/manager-list.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { CategoryNewComponent } from './categories/category-new/category-new.component';
import { ConstructorCompanyEditComponent } from './constructor-companies/constructor-company-edit/constructor-company-edit.component';
import { TeapotComponent } from './teapot/teapot.component';
import { CcadministratorsComponent } from './ccadministrators/ccadministrators.component';
import { ConstructorCompanyListComponent } from './constructor-companies/constructor-company-list/constructor-company-list.component';
import { ConstructorCompanyNewComponent } from './constructor-companies/constructor-company-new/constructor-company-new.component';
import { doesntHaveCompanyGuard } from './constructor-companies/doesnt-have-company.guard';
import { hasCompanyGuard } from './constructor-companies/constructor-company-edit/has-company.guard';
import { BuildingImportComponent } from './buildings/building-import/building-import.component';
import { ImporterComponent } from './importer/importer.component';
import { ImporterNewComponent } from './importer/importer-new/importer-new.component';
import { ImporterListComponent } from './importer/importer-list/importer-list.component';

const routes: Routes = [
  { path: '', component: AuthComponent, pathMatch: 'full', canActivate: [loggedGuard] },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] }, 
  { path: 'administrators', component: AdministratorsComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: AdministratorListComponent },
    { path: 'new', component: AdministratorNewComponent }
  ] },
  { path: 'invitations', component: InvitationsComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: InvitationListComponent },
    { path: 'new', component: InvitationNewComponent }
  ] },
  { path: 'invitations/:id', component: InvitationEditComponent },
  { path: 'managers', component: ManagersComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: ManagerListComponent },
  ] },
  { path: 'constructorCompanyAdministrators', component: CcadministratorsComponent, canActivate: [authGuard, administratorGuard] },
  { path: 'constructorCompanies', component: ConstructorCompaniesComponent, canActivate: [authGuard, constructorCompanyAdminGuard], children: [
    { path: '', component: ConstructorCompanyListComponent, canActivate: [doesntHaveCompanyGuard] },
    { path: 'new', component: ConstructorCompanyNewComponent, canActivate: [doesntHaveCompanyGuard] },
    { path: ':id', component: ConstructorCompanyEditComponent, canActivate: [hasCompanyGuard] },
  ] },
  { path: 'buildings', component: BuildingsComponent, canActivate: [authGuard], children: [
    { path: '', component: BuildingListComponent, canActivate: [buildingGuard] },
    { path: 'new', component: BuildingNewComponent, canActivate: [constructorCompanyAdminGuard, hasCompanyGuard]},
    { path: 'import', component: BuildingImportComponent, canActivate: [constructorCompanyAdminGuard, hasCompanyGuard] },
    { path: ':buildingId', component: BuildingFlatsComponent, canActivate: [managerGuard] },
    { path: ':buildingId/edit', component: BuildingEditComponent, canActivate: [constructorCompanyAdminGuard, hasCompanyGuard] },
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
  { path: 'categories', component: CategoriesComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: CategoryListComponent },
    { path: 'new', component: CategoryNewComponent }
  ] },
  { path: 'importers', component: ImporterComponent, canActivate: [authGuard, constructorCompanyAdminGuard, ], children: [
    { path: '', component: ImporterListComponent },
    { path: 'new', component: ImporterNewComponent }
  ] },
  { path: 'userSettings', component: UserSettingsComponent, canActivate: [authGuard] },
  { path: 'teapot', component: TeapotComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
