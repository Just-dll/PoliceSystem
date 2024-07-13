import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.scss']
})
export class ForgotComponent {
  email: string = '';

  constructor(private http: HttpClient) {}

  sendEmail() {
    console.log(this.email);
    // Here you can make an HTTP request to send the email
    // Replace 'your-api-endpoint' with your actual API endpoint
    this.http.post(`identity/forgotPassword`, { email: this.email }).subscribe(
      (response) => {
        console.log('Email sent successfully', response);
        // Optionally, you can show a success message to the user
      },
      (error) => {
        console.error('Error sending email', error);
        // Optionally, you can show an error message to the user
      }
    );
  }
}