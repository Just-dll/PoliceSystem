import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { RoleService } from './role.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private roleService: RoleService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const requiredRoles = route.data['requiredRoles'] as string[];
    if (this.roleService.hasAnyRole(requiredRoles)) {
      return true;
    } else {
      // Redirect to unauthorized page or handle accordingly
      this.router.navigate(['/unauthorized']);
      return false;
    }
  }
}
