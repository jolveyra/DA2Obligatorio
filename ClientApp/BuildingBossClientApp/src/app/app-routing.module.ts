import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { AdministratorsComponent } from './administrators/administrators.component';
import { AdministratorListComponent } from './administrators/administrator-list/administrator-list.component';
import { AdministratorNewComponent } from './administrators/administrator-new/administrator-new.component';
import { administratorGuard } from './administrators/administrator.guard';
import { authGuard } from './auth/auth.guard';
import { InvitationsComponent } from './invitations/invitations.component';
import { InvitationEditComponent } from './invitations/invitation-edit/invitation-edit.component';
import { InvitationListComponent } from './invitations/invitation-list/invitation-list.component';
import { InvitationNewComponent } from './invitations/invitation-new/invitation-new.component';
import { invitationGuard } from './invitations/invitation.guard';

const routes: Routes = [
  { path: '', component: AuthComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] }, 
  { path: 'administrators', component: AdministratorsComponent, canActivate: [authGuard, administratorGuard], children: [
    { path: '', component: AdministratorListComponent },
    { path: 'new', component: AdministratorNewComponent }
  ] },
  { path: 'invitations', component: InvitationsComponent, canActivate: [authGuard, invitationGuard], children: [
    { path: '', component: InvitationListComponent },
    { path: 'new', component: InvitationNewComponent },
    { path: ':id/edit', component: InvitationEditComponent }
  ] },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
