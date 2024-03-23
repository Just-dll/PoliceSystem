import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  userEntity: Object | undefined;
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  fetchUserEntity() {
    const accessToken = localStorage.getItem('accessToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${accessToken}`
    });

    this.http.get('https://localhost:7265/api/Person/getItself', { headers })
      .subscribe(
        (data : any) => {
          this.userEntity = data;
          console.log(data);
        },
        (error : any) => {
          console.error('Error fetching user data:', error);
        }
      );
  }
}
