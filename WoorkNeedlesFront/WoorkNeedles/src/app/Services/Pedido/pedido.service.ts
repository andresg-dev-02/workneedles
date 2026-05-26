import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface CreatePedidoDto {
  idCliente: number;
  idUsuario: number;
  fechEntregaAprox: string;
  fechaEntrega: string;
  direccionEntrega: string;
  observaciones: string;
  descuento: number | null;
}

export interface UpdatePedidoDto {
  fechEntregaAprox: string;
  fechaEntrega: string;
  direccionEntrega: string;
  observaciones: string;
  descuento: number | null;
}

export interface CreateDetallePedidoDto {
  idpedido: number;
  idproducto: number;
  idinventario: number | null;
  cantidad: number;
  preciounitario: number;
}

export interface PedidoDto {
  id: number;
  nombreCliente: string;
  nombreUsuario: string;
  fechapedido: string;
  fechentregaaprox: string;
  fechaentrega: string;
  fechamodificacion: string;
  estado: string;
  direccionentrega: string;
  observaciones: string;
  subtotal: number;
  descuento: number | null;
  total: number;
}

export interface UpdateDetallePedidoDto {
  idinventario: number | null;
  cantidad: number;
  preciounitario: number;
}

@Injectable({ providedIn: 'root' })
export class PedidoService {
  private apiUrl = environment.apiUrl + '/Pedido';

  constructor(private http: HttpClient) {}

  getPedidos(): Observable<PedidoDto[]> {
    return this.http.get<PedidoDto[]>(this.apiUrl);
  }

  getPedidoById(id: number): Observable<PedidoDto> {
    return this.http.get<PedidoDto>(`${this.apiUrl}/${id}`);
  }

  createPedido(dto: CreatePedidoDto): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }

  updatePedido(id: number, dto: UpdatePedidoDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deletePedido(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getPagos(idPedido: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${idPedido}/Pagos`);
  }


  
  getDetalles(idPedido: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${idPedido}/Detalles`);
  }

  addDetalle(idPedido: number, dto: CreateDetallePedidoDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/${idPedido}/Detalles`, dto);
  }

  updateDetalle(idPedido: number, idDetalle: number, dto: UpdateDetallePedidoDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${idPedido}/Detalles/${idDetalle}`, dto);
  }

  deleteDetalle(idPedido: number, idDetalle: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idPedido}/Detalles/${idDetalle}`);
  }

  cambiarEstado(idPedido: number, estado: string): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${idPedido}/estado`, { estado });
  }
}