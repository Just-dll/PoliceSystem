import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrivinglicensesComponent } from './drivinglicenses.component';

const routes: Routes = [{ path: '', component: DrivinglicensesComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DrivinglicensesRoutingModule { }
