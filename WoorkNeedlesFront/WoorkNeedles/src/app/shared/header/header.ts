import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/Auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  menuOpen = false;

  constructor(private router: Router, private authService: AuthService) {}

  get isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
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
