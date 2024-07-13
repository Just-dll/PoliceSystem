import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { RoleService } from './role.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private roleService: RoleService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const requiredRoles = route.data['requiredRoles'] as string[];
    return this.roleService.hasAnyRole(requiredRoles).pipe(
      map(hasRole => {
        if (hasRole) {
          return true;
        } else {
          // Redirect to unauthorized page or handle accordingly
          this.router.navigate(['/admin']);
          return false;
        }
      })
    );
  }
}
