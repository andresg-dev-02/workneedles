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
  estado: string;
  direccionentrega: string;
  observaciones: string;
  subtotal: number;
  descuento: number | null;
  total: number;
}

@Injectable({ providedIn: 'root' })
export class PedidoService {
  private apiUrl = environment.apiUrl + '/Pedido';

  constructor(private http: HttpClient) {}

  createPedido(dto: CreatePedidoDto): Observable<PedidoDto> {
    return this.http.post<PedidoDto>(this.apiUrl, dto);
  }

  addDetalle(idPedido: number, dto: CreateDetallePedidoDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/${idPedido}/Detalles`, dto);
  }

  getPedidos(): Observable<PedidoDto[]> {
    return this.http.get<PedidoDto[]>(this.apiUrl);
  }

  getPedidoById(id: number): Observable<PedidoDto> {
    return this.http.get<PedidoDto>(`${this.apiUrl}/${id}`);
  }
}