import { Component } from '@angular/core';
import { AuthService } from '../../../Services/Auth/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './login.component.html',
})
export class LoginComponent {

  email = '';
  password = '';
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login() {

    this.error = '';

    this.authService.login(
      this.email,
      this.password
    ).subscribe({

      next: (res) => {
        console.log('RESPUESTA LOGIN:', res);

        localStorage.setItem('token', res.token);

        this.router.navigate(['/productos']);

      },

      error: () => {

        this.error = 'Credenciales inválidas';

      }

    });

  }

  logout() {

    this.authService.logout();

    this.router.navigate(['/login']);

  }
}
