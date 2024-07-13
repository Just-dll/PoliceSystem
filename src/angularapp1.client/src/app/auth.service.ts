import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
    this.checkToken();
  }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post<any>(`/identity/login?useCookies=true`, { email, password }, { observe: 'response' })
      .pipe(
        map(response => {
          if (response) {
            this.loggedIn.next(true);
            return true;
          } else {
            console.log(response);
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
    this.http.post(`identity/logout`, {}).subscribe(() => {
      // Additional cleanup if needed
    });
  }

  isLoggedIn(): Observable<boolean> {
    return this.checkToken().pipe(
      tap((val: boolean) => {
        console.log('Token check result:', val);
        this.loggedIn.next(val);
      }),
      switchMap(() => this.loggedIn.asObservable())
    );
  }

  private setSession(authResult: any): void {
    this.loggedIn.next(true);
    // Calculate token expiration time and set a timer to refresh the token if needed
    const expiresIn = authResult.expiresIn * 1000; // Convert expiresIn to milliseconds
    setTimeout(() => {
      this.refreshToken().subscribe();
    }, expiresIn - 60000); // Refresh the token 1 minute before it expires
  }

  checkToken(): Observable<boolean> {
    return this.http.get<any>(`identity/Person/getMyself`).pipe(
      map(data => {
        return !!data;
      }),
      catchError(error => {
        console.error('Error fetching user data:', error);
        return of(false);
      })
    );
  }

  private refreshToken(): Observable<any> {
    return this.http.post<any>(`identity/refresh`, {})
      .pipe(
        switchMap(response => {
          if (response && response.accessToken) {
            this.setSession(response);
            return of(true);
          } else {
            console.error('Failed to refresh token.');
            this.logout();
            return of(false);
          }
        }),
        catchError(error => {
          console.error('Error occurred during token refresh:', error);
          this.logout();
          return of(false);
        })
      );
  }
}
