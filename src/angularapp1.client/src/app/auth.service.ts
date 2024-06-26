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
    this.checkToken();
  }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post<any>(`${environment.baseApiUrl}/login?useCookies=true`, { email, password }, { observe: 'response' })
      .pipe(
        map(response => {
          if(response) {
            this.loggedIn.next(true);
            return true;
          }
          else {
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
    this.http.post(`${environment.baseApiUrl}/logout`, {}).subscribe(() => {
      // Additional cleanup if needed
    });
  }

  isLoggedIn(): Observable<boolean> {
    this.checkToken()
    return this.loggedIn.asObservable();
  }

  private setSession(authResult: any): void {
    this.loggedIn.next(true);
    // Calculate token expiration time and set a timer to refresh the token if needed
    const expiresIn = authResult.expiresIn * 1000; // Convert expiresIn to milliseconds
    setTimeout(() => {
      this.refreshToken().subscribe();
    }, expiresIn - 60000); // Refresh the token 1 minute before it expires
  }

  private checkToken(): boolean {
    var state = false;
    this.http.get(`${environment.baseApiUrl}/api/Person/getMyself`)
      .subscribe(
        data => {
          if (data) {
            this.loggedIn.next(true);
            state = true;
            console.log(data);
            return true;
          } else {
            this.loggedIn.next(false);
            return false;
          }
        },
        error => {
          console.error('Error fetching user data:', error);
          this.loggedIn.next(false);
        }
      );
      return state;
  }


  private refreshToken(): Observable<any> {
    return this.http.post<any>(`${environment.baseApiUrl}/refresh`, {})
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
