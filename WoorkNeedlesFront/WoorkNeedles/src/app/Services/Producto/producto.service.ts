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

export interface FiltroProductoDto {
  nombre?: string;
  categoria?: string;
  talla?: string;
  color?: string;
  material?: string;
  genero?: string;
  precioMin?: number;
  precioMax?: number;
  disponible?: boolean;
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

  createInsumoProducto(idProducto: number, dto: { idinsumo: number; cantidad: number }): Observable<any> {
    return this.http.post(`${this.apiUrl}/${idProducto}/Insumos`, { ...dto, idproducto: idProducto });
  }

  updateInsumoProducto(idProducto: number, id: number, cantidad: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${idProducto}/Insumos/${id}`, { cantidad });
  }

  deleteInsumoProducto(idProducto: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idProducto}/Insumos/${id}`);
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

  buscarProductos(filtro: FiltroProductoDto): Observable<ProductoModel[]> {
    const params: any = {};
    if (filtro.nombre) params.nombre = filtro.nombre;
    if (filtro.categoria) params.categoria = filtro.categoria;
    if (filtro.talla) params.talla = filtro.talla;
    if (filtro.color) params.color = filtro.color;
    if (filtro.material) params.material = filtro.material;
    if (filtro.genero) params.genero = filtro.genero;
    if (filtro.precioMin != null) params.precioMin = filtro.precioMin;
    if (filtro.precioMax != null) params.precioMax = filtro.precioMax;
    if (filtro.disponible != null) params.disponible = filtro.disponible;
    return this.http.get<ProductoModel[]>(`${this.apiUrl}/buscar`, { params });
  }
}