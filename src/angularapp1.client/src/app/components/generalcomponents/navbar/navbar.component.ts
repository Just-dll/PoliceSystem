import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../auth.service';
import { RoleService } from '../../../role.service';
import { Roles } from '../../../roles';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  standalone: true
})
export class NavbarComponent implements OnInit {
  isAuth: boolean = false;
  userRoles: string[] = [];
  Roles = Roles;

  constructor(private authService: AuthService, public roleService: RoleService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn().subscribe((isAuthenticated: boolean) => {
      this.isAuth = isAuthenticated;
    });
    this.roleService.getUserRoles().subscribe((things: string[]) => {
      this.userRoles = things;
    })
  }
}
