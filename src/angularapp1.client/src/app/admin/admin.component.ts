import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DrivingLicense } from './drivinglicenses/drivinglicense/drivinglicense';
import { Router } from '@angular/router';
import { UserSearchResult } from './usersearchresult';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  userRoles: string | undefined;
  userEntity: any;
  drivingLicense: DrivingLicense | undefined;
  searchResults: UserSearchResult[] = [];
  showResults: boolean = false;
  eventSource: EventSource | undefined;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.fetchUser();
    this.fetchUserDrivingLicense();
    //this.initializeSSE();
  }

  searchTerm: string = '';

  initializeSSE() {
    this.eventSource = new EventSource(`${environment.baseApiUrl}/stream`, { withCredentials : true });
    console.log("InitSSE");
    this.eventSource.onmessage = (event) => {
      const data = JSON.parse(event.data);
      console.log('New message received:', data);
    };

    this.eventSource.onerror = (error) => {
      console.error('Error in SSE connection:', error);
    };
  }

  redirectToUserProfile(userId: number): void { 
    this.router.navigate(['/admin/userprofile', userId]);
  }

  search(): void {
    this.searchUser(this.searchTerm);
  }

  private searchUser(query: string) {
    this.http.get<UserSearchResult[]>(`identity/Person/SearchByQuery`, { params: { query } })
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
    this.http.get<DrivingLicense>(`api/DrivingLicense/getmydrivinglicense`)
      .subscribe(
        (data) => {
          this.drivingLicense = data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }

  

  fetchUser() {
    this.http.get(`identity/Person/getMyself`)
      .subscribe(
        (data) => {
          this.userEntity = data;
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
}
