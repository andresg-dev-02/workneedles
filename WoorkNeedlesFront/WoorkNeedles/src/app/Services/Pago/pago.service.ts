import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface PagoDto {
  id: number;
  nombreUsuario: string;
  fechapago: string | null;
  monto: number;
  tipopago: string;
  estado: string;
  referencia?: string;
  observaciones?: string;
}

export interface CreatePagoDto {
  idpedido: number;
  idusuario: number;
  monto: number;
  tipopago: string;
  referencia?: string | null;
  observaciones?: string | null;
}

export interface CambiarEstadoPagoDto {
  estado: string;
}

@Injectable({ providedIn: 'root' })
export class PagoService {
  private apiUrl = environment.apiUrl + '/Pedido';

  constructor(private http: HttpClient) {}

  getPagos(idPedido: number): Observable<PagoDto[]> {
    return this.http.get<PagoDto[]>(`${this.apiUrl}/${idPedido}/Pagos`);
  }

  createPago(idPedido: number, dto: CreatePagoDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/${idPedido}/Pagos`, dto);
  }

  cambiarEstado(idPedido: number, id: number, dto: CambiarEstadoPagoDto): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${idPedido}/Pagos/${id}/estado`, dto);
  }

  deletePago(idPedido: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idPedido}/Pagos/${id}`);
  }
}