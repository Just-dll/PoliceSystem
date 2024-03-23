import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit {
  userJson: string | undefined;
  userEntity: Object | undefined
  constructor(private http: HttpClient) { }
  ngOnInit() {
    this.fetchUserRoles();
  }
  
  fetchUserRoles() {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get('https://localhost:7265/api/PolicePositions', { headers })
      .subscribe(
        (data) => {
          this.userJson = JSON.stringify(data);
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
  
}
