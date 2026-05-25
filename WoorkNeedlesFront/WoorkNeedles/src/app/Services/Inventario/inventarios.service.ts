import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface InventarioDto {
  id: number;
  nombreProducto: string;
  nombreColor: string;
  nombreTalla: string;
  stock: number;
}

export interface CreateInventarioDto {
  idproducto: number;
  idcolor: number;
  idtalla: number;
  stock: number;
}

export interface UpdateInventarioDto {
  stock: number;
}

@Injectable({ providedIn: 'root' })
export class InventarioService {
  private apiUrl = environment.apiUrl + '/Producto';

  constructor(private http: HttpClient) {}

  getInventarioPorProducto(idProducto: number): Observable<InventarioDto[]> {
    return this.http.get<InventarioDto[]>(`${this.apiUrl}/${idProducto}/Inventario`);
  }

  createInventario(idProducto: number, dto: CreateInventarioDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/${idProducto}/Inventario`, dto);
  }

  updateInventario(idProducto: number, id: number, dto: UpdateInventarioDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${idProducto}/Inventario/${id}`, dto);
  }

  deleteInventario(idProducto: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idProducto}/Inventario/${id}`);
  }
}