import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { TicketsComponent } from './tickets/tickets.component';
import { TicketComponent } from './tickets/ticket/ticket.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { PrettyPipe } from '../pretty.pipe';


@NgModule({
  declarations: [
    AdminComponent,
    TicketsComponent,
    TicketComponent,
    SidebarComponent,
    PrettyPipe
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
