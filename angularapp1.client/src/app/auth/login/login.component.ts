import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router : Router) { }

  login(): void {
    this.authService.login(this.email, this.password).subscribe((loggedIn: boolean) => {
      if (loggedIn) {
        console.log(loggedIn);
        this.router.navigate(['/admin'])
        console.log('Login successful');
      } else {
        console.log('Invalid email or password');
      }
    });
  }
}
