import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DrivinglicensesRoutingModule } from './drivinglicenses-routing.module';
import { DrivinglicensesComponent } from './drivinglicenses.component';


@NgModule({
  declarations: [
    DrivinglicensesComponent
  ],
  imports: [
    CommonModule,
    DrivinglicensesRoutingModule
  ]
})
export class DrivinglicensesModule { }
