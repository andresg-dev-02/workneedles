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

export interface InsumosProductoDto {
  id: number;
  nombreProducto: string;
  nombreInsumo: string;
  unidadMedida: string;
  cantidad: number;
}

export interface CreateProductoDto {
  nombre: string;
  descripcion: string;
  material?: string;
  preciobase: number;
  urlimagen?: string;
  idCategoria: number;
  genero?: string;
  activo?: boolean;
}

export type UpdateProductoDto = CreateProductoDto;

@Injectable({ providedIn: 'root' })
export class ProductoService {
  private apiUrl = environment.apiUrl + '/Producto';

  constructor(private http: HttpClient) {}

  getProductos(): Observable<ProductoModel[]> {
    return this.http.get<ProductoModel[]>(this.apiUrl);
  }

  getProductoById(id: number): Observable<ProductoModel> {
    return this.http.get<ProductoModel>(`${this.apiUrl}/${id}`);
  }

  getInventario(idProducto: number): Observable<InventarioDto[]> {
    return this.http.get<InventarioDto[]>(`${this.apiUrl}/${idProducto}/Inventario`);
  }

  getInsumos(idProducto: number): Observable<InsumosProductoDto[]> {
    return this.http.get<InsumosProductoDto[]>(`${this.apiUrl}/${idProducto}/Insumos`);
  }

  createProducto(dto: CreateProductoDto): Observable<void> {
    return this.http.post<void>(this.apiUrl, dto);
  }

  updateProducto(id: number, dto: UpdateProductoDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  deleteProducto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}