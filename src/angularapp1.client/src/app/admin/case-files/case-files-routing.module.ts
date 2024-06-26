import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CaseFilesComponent } from './case-files.component';
import { CaseFileComponent } from './case-file/case-file.component';

const routes: Routes = [
  { 
    path: '', component: CaseFilesComponent 
  },
  { 
    path: ':id', component: CaseFileComponent 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaseFilesRoutingModule { }
