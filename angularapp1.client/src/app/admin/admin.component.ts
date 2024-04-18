import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DrivingLicense } from './drivinglicenses/drivinglicense/drivinglicense';
import { Router } from '@angular/router';
import { UserSearchResult } from './usersearchresult';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit {
  userRoles: string | undefined;
  userEntity: Object | undefined;
  drivingLicense: DrivingLicense | undefined;
  searchResults: UserSearchResult[] = [];
  showResults: boolean = false;
  constructor(private http: HttpClient, private router : Router) { }
  ngOnInit() {
    this.fetchUserRoles();
    this.fetchUser();
    this.fetchUserDrivingLicense()
  }
  searchTerm: string = '';

  redirectToUserProfile(userId: number): void {
    // Redirect to the user profile page with the user's ID in the URL
    this.router.navigate(['/admin/userprofile', userId]);
  }

  search(): void {
    // Here, you can perform any action with the searchTerm, such as searching a database or displaying results
    this.searchUser(this.searchTerm);
  }
  private searchUser(query: string) {
    const accessToken = localStorage.getItem('accessToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get<UserSearchResult[]>(`${environment.baseApiUrl}/api/Person/SearchByQuery`, { params: { query }, headers })
      .subscribe(
        (data) => {
          console.log('Search results:', data);
          this.searchResults = data;
          this.showResults = true;
        },
        (error) => {
          console.error('Error searching user:', error);
        }
      );
  }

  fetchUserDrivingLicense() {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get<DrivingLicense>(`${environment.baseApiUrl}/api/DrivingLicense/getmydrivinglicense`, { headers })
      .subscribe(
        (data) => {
          this.drivingLicense = data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }

  fetchUserRoles() {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get(`${environment.baseApiUrl}/api/PolicePositions`, { headers })
      .subscribe(
        (data) => {
          this.userRoles = JSON.stringify(data);
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
  
  fetchUser() {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get(`${environment.baseApiUrl}/api/getMyself`, { headers })
      .subscribe(
        (data) => {
          this.userEntity = JSON.stringify(data);
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }

  authorizedRequest<T = any>(endpoint : string) {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get<T>(`${environment.baseApiUrl}${endpoint}`, { headers })
      .subscribe(
        (data) => {
          return data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
}
