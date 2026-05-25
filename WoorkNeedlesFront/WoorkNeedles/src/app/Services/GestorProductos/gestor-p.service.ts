import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductoModel } from '../../Models/Producto/producto.model';


export interface InsumosProductoDto {
  id: number;
  nombreProducto: string;
  nombreInsumo: string;
  unidadMedida: string;
  cantidad: number;
}

@Injectable({
  providedIn: 'root',
})
export class GestorPService {

 private apiUrl = environment.apiUrl + '/Producto';
constructor(private http: HttpClient) {}

  getProductos(): Observable<ProductoModel[]> {
    return this.http.get<ProductoModel[]>(`${this.apiUrl}/Producto`);
  }

  getInsumos(idProducto: number): Observable<InsumosProductoDto[]> {
    return this.http.get<InsumosProductoDto[]>(
      `${this.apiUrl}/Producto/${idProducto}/Insumos`
    );
  }
}
