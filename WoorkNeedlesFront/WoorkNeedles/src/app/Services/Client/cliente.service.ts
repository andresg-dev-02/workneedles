import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ClienteDto {
  id: number;
  tipocliente: string;
  tipodocumento: string;
  documento: string;
  nombres: string | null;
  apellidos: string | null;
  razonsocial: string | null;
  email: string;
  telefono: string;
  direccion: string;
  preferenciasCompra: string | null;
  activo: boolean;
  pais: string;
  departamento: string;
  ciudad: string;
}

@Injectable({ providedIn: 'root' })
export class ClienteService {
  private apiUrl = environment.apiUrl + '/Cliente';

  constructor(private http: HttpClient) {}

  getClientes(): Observable<ClienteDto[]> {
    return this.http.get<ClienteDto[]>(this.apiUrl);
  }
}