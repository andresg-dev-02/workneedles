import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductoModel } from '../../Models/Producto/producto.model';

export interface InventarioDto {
  id: number;
  nombreProducto: string;
  nombreColor: string;
  nombreTalla: string;
  stock: number;
}

@Injectable({
  providedIn: 'root',
})
export class ProductoService {
  private apiUrl = environment.apiUrl + '/Producto';

  constructor(private http: HttpClient) {}

  getProductos(): Observable<ProductoModel[]> {
    return this.http.get<ProductoModel[]>(this.apiUrl);
  }

  getInventario(idProducto: number): Observable<InventarioDto[]> {
    return this.http.get<InventarioDto[]>(`${this.apiUrl}/${idProducto}/Inventario`);
  }
}