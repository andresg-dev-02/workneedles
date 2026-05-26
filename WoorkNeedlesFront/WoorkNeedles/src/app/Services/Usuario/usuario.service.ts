import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface UsuarioDto {
  id: number;
  nombres: string;
  apellidos: string;
  email: string;
  telefono: string;
  activo: boolean;
  fechacreacion: string;
  fechamodificacion: string | null;
  rol: string;
  pais: string;
  ciudad: string;
}

export interface CreateUsuarioDto {
  nombres: string;
  apellidos: string;
  email: string;
  contrasena: string;
  telefono: string;
  idRol: number;
  idPais: number;
  idCiudad: number;
}

export interface UpdateUsuarioDto extends CreateUsuarioDto {
  contrasenaNueva?: string | null;
  activo: boolean;
}

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private apiUrl = environment.apiUrl + '/User';

  constructor(private http: HttpClient) {}

  getUsuarios(): Observable<UsuarioDto[]> {
    return this.http.get<UsuarioDto[]>(this.apiUrl);
  }

  createUsuario(dto: CreateUsuarioDto): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }

  updateUsuario(id: number, dto: UpdateUsuarioDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteUsuario(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}