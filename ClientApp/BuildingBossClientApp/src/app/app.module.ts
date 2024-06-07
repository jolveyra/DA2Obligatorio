import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { LoadingSpinnerComponent } from './shared/loading-spinner/loading-spinner.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AdministratorsComponent } from './administrators/administrators.component';
import { AdministratorListComponent } from './administrators/administrator-list/administrator-list.component';
import { AdministratorItemComponent } from './administrators/administrator-list/administrator-item/administrator-item.component';
import { AdministratorNewComponent } from './administrators/administrator-new/administrator-new.component';
import { BackIconComponent } from './shared/back-icon/back-icon.component';
import { InvitationsComponent } from './invitations/invitations.component';
import { InvitationListComponent } from './invitations/invitation-list/invitation-list.component';
import { InvitationItemComponent } from './invitations/invitation-list/invitation-item/invitation-item.component';
import { InvitationNewComponent } from './invitations/invitation-new/invitation-new.component';
import { InvitationEditComponent } from './invitations/invitation-edit/invitation-edit.component';
import { BuildingsComponent } from './buildings/buildings.component';
import { BuildingListComponent } from './buildings/building-list/building-list.component';
import { BuildingItemComponent } from './buildings/building-list/building-item/building-item.component';
import { BuildingEditComponent } from './buildings/building-edit/building-edit.component';
import { BuildingNewComponent } from './buildings/building-new/building-new.component';
import { BuildingFlatsComponent } from './buildings/building-flats/building-flats.component';
import { BuildingFlatEditComponent } from './buildings/building-flat-edit/building-flat-edit.component';
import { FlatItemComponent } from './buildings/building-flats/flat-item/flat-item.component';
import { RequestsComponent } from './requests/requests.component';
import { RequestListComponent } from './requests/request-list/request-list.component';
import { RequestItemComponent } from './requests/request-list/request-item/request-item.component';
import { RequestNewComponent } from './requests/request-new/request-new.component';
import { RequestEditComponent } from './requests/request-edit/request-edit.component';
import { MaintenanceEmployeesComponent } from './maintenance-employees/maintenance-employees.component';
import { EmployeeListComponent } from './maintenance-employees/employee-list/employee-list.component';
import { EmployeeItemComponent } from './maintenance-employees/employee-list/employee-item/employee-item.component';
import { EmployeeNewComponent } from './maintenance-employees/employee-new/employee-new.component';
import { ReportsComponent } from './reports/reports.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { ConstructorCompaniesComponent } from './constructor-companies/constructor-companies.component';
import { AuthInterceptorService } from './shared/auth-interceptor.service';
import { ManagersComponent } from './managers/managers.component';
import { ManagerListComponent } from './managers/manager-list/manager-list.component';
import { ManagerItemComponent } from './managers/manager-list/manager-item/manager-item.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { CategoryItemComponent } from './categories/category-list/category-item/category-item.component';
import { CategoryNewComponent } from './categories/category-new/category-new.component';
import { ConstructorCompanyNewComponent } from './constructor-companies/constructor-company-new/constructor-company-new.component';
import { ConstructorCompanyListComponent } from './constructor-companies/constructor-company-list/constructor-company-list.component';
import { ConstructorCompanyEditComponent } from './constructor-companies/constructor-company-edit/constructor-company-edit.component';
import { ConstructorCompanyItemComponent } from './constructor-companies/constructor-company-item/constructor-company-item.component';
import { TeapotComponent } from './teapot/teapot.component';
import { CcadministratorsComponent } from './ccadministrators/ccadministrators.component';
import { CcadministratorItemComponent } from './ccadministrators/ccadministrator-item/ccadministrator-item.component';
import { BuildingImportComponent } from './buildings/building-import/building-import.component';
import { ImporterComponent } from './importer/importer.component';
import { ImporterNewComponent } from './importer/importer-new/importer-new.component';
import { ImporterListComponent } from './importer/importer-list/importer-list.component';
import { ImportItemComponent } from './importer/importer-list/import-item/import-item.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    LoadingSpinnerComponent,
    HeaderComponent,
    HomeComponent,
    SidebarComponent,
    AdministratorsComponent,
    AdministratorListComponent,
    AdministratorItemComponent,
    AdministratorNewComponent,
    BackIconComponent,
    InvitationsComponent,
    InvitationListComponent,
    InvitationItemComponent,
    InvitationNewComponent,
    InvitationEditComponent,
    BuildingsComponent,
    BuildingListComponent,
    BuildingItemComponent,
    BuildingEditComponent,
    BuildingNewComponent,
    BuildingFlatsComponent,
    BuildingFlatEditComponent,
    FlatItemComponent,
    RequestsComponent,
    RequestListComponent,
    RequestItemComponent,
    RequestNewComponent,
    RequestEditComponent,
    MaintenanceEmployeesComponent,
    EmployeeListComponent,
    EmployeeItemComponent,
    EmployeeNewComponent,
    ReportsComponent,
    UserSettingsComponent,
    ConstructorCompaniesComponent,
    ManagersComponent,
    ManagerListComponent,
    ManagerItemComponent,
    CategoriesComponent,
    CategoryListComponent,
    CategoryItemComponent,
    CategoryNewComponent,
    ConstructorCompanyNewComponent,
    ConstructorCompanyListComponent,
    ConstructorCompanyEditComponent,
    ConstructorCompanyItemComponent,
    TeapotComponent,
    CcadministratorsComponent,
    CcadministratorItemComponent,
    BuildingImportComponent,
    ImporterComponent,
    ImporterNewComponent,
    ImporterListComponent,
    ImportItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
