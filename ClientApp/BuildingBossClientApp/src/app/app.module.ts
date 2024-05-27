import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { LoadingSpinnerComponent } from './shared/loading-spinner/loading-spinner.component';
import { HttpClientModule } from '@angular/common/http';
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
    FlatItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
