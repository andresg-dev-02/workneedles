import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/Auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './header.html',
})
export class Header {
  menuOpen = false;

  constructor(private router: Router, private authService: AuthService) {}

  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  get isLoggedIn(): boolean {
  return this.authService.isAuthenticated();
}

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  get isEmpleado(): boolean {
    return this.authService.getRol() === 'Empleado';
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/productos']);
  }

  navigateTo(route: string) {
    this.menuOpen = false;
    this.router.navigate([route]);
  }
}