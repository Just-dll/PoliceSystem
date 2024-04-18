import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
    

    // Check if the user is already logged in from a previous session
    this.checkToken();
    localStorage.clear();
  }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post<any>(`${environment.baseApiUrl}/login`, { email, password })
      .pipe(
        map(response => {
          if (response && response.accessToken) {
            this.setSession(response);
            return true;
          } else {
            this.loggedIn.next(false);
            return false;
          }
        }),
        catchError(error => {
          console.error('Error occurred during login:', error);
          this.loggedIn.next(false);
          return of(false);
        })
      );
  }

  logout(): void {
    this.loggedIn.next(false);
    localStorage.clear();
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  private setSession(authResult: any): void {
    localStorage.setItem('loggedIn', 'true');
    localStorage.setItem('accessToken', authResult.accessToken);
    localStorage.setItem('expiresIn', authResult.expiresIn);
    localStorage.setItem('refreshToken', authResult.refreshToken);
    localStorage.setItem('tokenType', authResult.tokenType);
    this.loggedIn.next(true);
    // Calculate token expiration time and set a timer to refresh the token if needed
    const expiresIn = authResult.expiresIn * 1000; // Convert expiresIn to milliseconds
    setTimeout(() => {
      this.refreshToken().subscribe();
    }, expiresIn - 60000); // Refresh the token 1 minute before it expires
  }

  private checkToken(): void {
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      this.loggedIn.next(true);
      const expiresIn = Number(localStorage.getItem('expiresIn'));
      const now = Date.now();
      if (expiresIn && expiresIn - now < 60000) { // Check if token expires in less than 1 minute
        this.refreshToken().subscribe();
      }
    }
  }

  private refreshToken(): Observable<any> {
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) {
      console.error('Refresh token not found.');
      return of(null);
    }
    return this.http.post<any>('https://localhost:7265/refresh', { refreshToken })
      .pipe(
        switchMap(response => {
          if (response && response.accessToken) {
            this.setSession(response);
          } else {
            console.error('Failed to refresh token.');
            this.logout();
          }
          return of(null);
        }),
        catchError(error => {
          console.error('Error occurred during token refresh:', error);
          this.logout();
          return of(null);
        })
      );
  }
}
