import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { TicketsComponent } from './tickets/tickets.component';
import { RoleGuard } from '../role.guard';
import { Roles } from '../roles';

const routes: Routes = [
  { 
    path: '', 
    component: AdminComponent,
  },
  {
    path: 'tickets',
    component: TicketsComponent
  },
  { 
    path: 'drivinglicenses', 
    loadChildren: () => import('./drivinglicenses/drivinglicenses.module').then(m => m.DrivinglicensesModule) 
  },
  { 
    path: 'userprofile/:id',
    canActivate: [RoleGuard], 
    data: {
      requiredRoles: [Roles.POLICEMAN, Roles.JUDGE, Roles.PROSECUTOR] // Example roles
    },
    loadChildren: () => import('./user-profile/user-profile.module').then(m => m.UserProfileModule) 
  },
  { 
    path: 'caseFiles',
    canActivate: [RoleGuard],
    data: {
      requiredRoles: [Roles.JUDGE, Roles.PROSECUTOR] 
    },
    loadChildren: () => import('./case-files/case-files.module').then(m => m.CaseFilesModule)
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
