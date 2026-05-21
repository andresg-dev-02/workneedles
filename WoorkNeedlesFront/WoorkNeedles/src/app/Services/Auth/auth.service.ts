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
        localStorage.setItem(
          'expiracion',
          response.expiracion
        );

      })

    );

  }

  logout() {

    localStorage.removeItem('token');
    localStorage.removeItem('expiracion');

  }

  getToken(): string | null {

    return localStorage.getItem('token');

  }

  isAuthenticated(): boolean {

    return !!localStorage.getItem('token');

  }



}
