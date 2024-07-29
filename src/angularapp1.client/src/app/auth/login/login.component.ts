import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router : Router, private snackBar: MatSnackBar) { }

  login(): void {
    this.authService.login(this.email, this.password).subscribe((loggedIn: boolean) => {
      if (loggedIn) {
        this.router.navigate(['/admin'])
      } else {
        this.showErrorMessage('Invalid email or password');
      }
    });
  }

  showErrorMessage(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }
}
