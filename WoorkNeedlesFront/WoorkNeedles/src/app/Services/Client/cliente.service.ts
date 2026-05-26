import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ClienteDto {
  id: number;
  idPais: number;      
  idDepart: number;    
  idCiudad: number;
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
  fechacreacion: string | null;
  fechamodificacion: string | null;
}

@Injectable({ providedIn: 'root' })
export class ClienteService {
  private apiUrl = environment.apiUrl + '/Cliente';

  constructor(private http: HttpClient) {}

  getUbicacion(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/Ubication`);
  }

  createCliente(dto: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateCliente(id: number, dto: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  getClientes(): Observable<ClienteDto[]> {
    return this.http.get<ClienteDto[]>(this.apiUrl);
  }

  deleteCliente(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}