import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../../environments/environment.development';
import { CaseFileModel, ReportModel } from './case-file-model';

@Component({
  selector: 'app-case-file',
  templateUrl: './case-file.component.html',
  styleUrl: './case-file.component.scss'
})
export class CaseFileComponent {
  caseFile: CaseFileModel | null = null;
  isReportFormOpen = false;
  newReport: Partial<ReportModel> = {};

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.http.get<CaseFileModel>(`${environment.baseApiUrl}/api/Case?caseFileId=${id}`).subscribe((data: CaseFileModel) => {
        this.caseFile = data;
      });
    }
  }

  openReportForm(): void {
    this.isReportFormOpen = true;
  }

  closeReportForm(): void {
    this.isReportFormOpen = false;
  }

  submitReport(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && this.newReport.reportedLocation && this.newReport.description) {
      this.http.post<ReportModel>(`${environment.baseApiUrl}/api/Report`, {
        caseFileId: id,
        ...this.newReport
      }).subscribe((report: ReportModel) => {
        if (this.caseFile) {
          this.caseFile.reports.push(report);
        }
        this.closeReportForm();
      });
    }
  }
}
