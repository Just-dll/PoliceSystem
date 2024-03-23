import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './auth/login/login.component';
import { AdminpanelComponent } from './adminpanel/adminpanel.component';
import { TicketComponent } from './admin/tickets/ticket/ticket.component';
import { RegisterComponent } from './auth/register/register.component';
import { ForgotComponent } from './auth/forgot/forgot.component';
import { GrantedToDirective } from './granted-to.directive';
import { PrettyPipe } from './pretty.pipe';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminpanelComponent,
    RegisterComponent,
    ForgotComponent,
    GrantedToDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavbarComponent,
    FooterComponent,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
