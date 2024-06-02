import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaseFilesRoutingModule } from './case-files-routing.module';
import { CaseFilesComponent } from './case-files.component';


@NgModule({
  declarations: [
    CaseFilesComponent
  ],
  imports: [
    CommonModule,
    CaseFilesRoutingModule
  ]
})
export class CaseFilesModule { }
