import { Component, Input } from '@angular/core';
import { CaseFilePreview } from '../../usersearchresult';
import { Router } from '@angular/router';

@Component({
  selector: 'app-case-file-preview',
  templateUrl: './case-file-preview.component.html',
  styleUrl: './case-file-preview.component.scss'
})
export class CaseFilePreviewComponent {
  @Input() caseFile!: CaseFilePreview;

  constructor(private router: Router) {}
  
  formatSuspects(): string {
    return this.caseFile.suspects.map(s => s.name).join(', ');
  }

  viewCaseFile() {
    this.router.navigate(['/admin/caseFiles', this.caseFile.id]);
  }
}
