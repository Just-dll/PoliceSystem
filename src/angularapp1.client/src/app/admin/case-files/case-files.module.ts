import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaseFilesRoutingModule } from './case-files-routing.module';
import { CaseFilesComponent } from './case-files.component';
import { CaseFileComponent } from './case-file/case-file.component';
import { CaseFilePreviewComponent } from './case-file-preview/case-file-preview.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    CaseFilesComponent,
    CaseFileComponent,
    CaseFilePreviewComponent
  ],
  imports: [
    CommonModule,
    CaseFilesRoutingModule,
    FormsModule
  ]
})
export class CaseFilesModule { }
