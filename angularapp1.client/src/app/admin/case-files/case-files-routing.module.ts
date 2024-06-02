import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CaseFilesComponent } from './case-files.component';

const routes: Routes = [{ path: '', component: CaseFilesComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaseFilesRoutingModule { }
