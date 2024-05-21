import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
  userProfile: any;
  userTickets: any;
  userDrivingLicense: any;
  constructor(
    private route: ActivatedRoute,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    const Qid = this.route.snapshot.paramMap.get('id') ?? 0; // default value 0
    const id = +Qid;
    this.getUserProfile(id);
    this.getUserDrivingLicense(id);
    this.getUserTickets(id);
  }

  getUserProfile(id: number): void {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get(`${environment.baseApiUrl}/api/Person/getUser?id=${id}`, { headers })
      .subscribe(
        (data) => {
          this.userProfile = data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
  getUserTickets(id: number): void {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get(`${environment.baseApiUrl}/api/Tickets/personTickets?id=${id}`, { headers })
      .subscribe(
        (data) => {
          this.userTickets = data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
  getUserDrivingLicense(id: number): void {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get(`${environment.baseApiUrl}/api/DrivingLicense/GetPersonDrivingLicense?id=${id}`, { headers })
      .subscribe(
        (data) => {
          this.userDrivingLicense = data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
}
