import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
  private apiUrl = environment.apiUrl + '/Auth';

  constructor(private http: HttpClient) {}

  login(email: string, contrasena: string): Observable<any> {

    return this.http.post<any>(
      `${this.apiUrl}/login`,
      {
        email,
        contrasena
      }
    ).pipe(

      tap((response) => {

        localStorage.setItem('token', response.token);
        localStorage.setItem('expiracion',response.expiracion);
        
        const rol = this.extractRolFromToken(response.token);
        console.log(rol);
        if (rol) localStorage.setItem('rol', rol);
      })

    );
  }

  private extractRolFromToken(token: string): string | null {
    try {
      const payload = token.split('.')[1];
      const decoded = JSON.parse(atob(payload));

      return (
        decoded['role'] ||
        decoded['roles'] ||
        decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
        null
      );
    } catch {
      return null;
    }
  }

  getUsuarioId(): number {
  try {
    const token = this.getToken();
    if (!token) return 0;
    const payload = JSON.parse(atob(token.split('.')[1]));
    return parseInt(
      payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || '0'
    );
  } catch {
    return 0;
  }
}

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('expiracion');
    localStorage.removeItem('rol')
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  getRol(): string | null {
    return localStorage.getItem('rol');
  }

  isAdmin(): boolean {
    return this.getRol() === 'Administrador';
  }




}
