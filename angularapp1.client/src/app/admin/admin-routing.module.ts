import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { TicketsComponent } from './tickets/tickets.component';

const routes: Routes = [
  { 
    path: '', 
    component: AdminComponent 
  },
  {
    path: 'tickets',
    component: TicketsComponent
  },
  { path: 'drivinglicenses', loadChildren: () => import('./drivinglicenses/drivinglicenses.module').then(m => m.DrivinglicensesModule) },
  { path: 'profile', loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule) }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
