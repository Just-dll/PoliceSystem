import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { CaseFilePreview, UserSearchResult } from '../usersearchresult';

@Component({
  selector: 'app-case-files',
  templateUrl: './case-files.component.html',
  styleUrls: ['./case-files.component.scss']
})
export class CaseFilesComponent implements OnInit {
  caseFiles: CaseFilePreview[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<CaseFilePreview[]>(`api/Case/mine`).subscribe((data: CaseFilePreview[]) => {
      this.caseFiles = data;
    });
  }
}
