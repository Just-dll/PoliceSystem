import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  constructor(private http: HttpClient) { }

  hasAnyRole(requiredRoles: string[]): Observable<boolean> {
    return this.getUserRoles().pipe(
      map(userRoles => requiredRoles.some(role => userRoles.includes(role)))
    );
  }

  getUserRoles(): Observable<string[]> {
    return this.http.get<string[]>(`${environment.baseApiUrl}/api/PolicePositions/getMyPositions`);
  }
  
}
