import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './auth.guard';
import { RegisterComponent } from './auth/register/register.component';
import { ForgotComponent } from './auth/forgot/forgot.component';


const routes: Routes = [
  { 
    path: 'login', 
    component: LoginComponent 
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'forgot',
    component: ForgotComponent
  },
  { 
    path: 'admin', 
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule), 
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard]
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes),HttpClientModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
