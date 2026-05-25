import { Injectable, OnInit } from '@angular/core';
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

@Injectable({
  providedIn: 'root',
})
export class InventarioService implements OnInit
{
  private apiUrl = environment.apiUrl + '/Inventario';

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  getInventarios(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`);
  }

  createInventario(dto: CreateInventarioDto): Observable<any> {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateInventario(id: number, dto: UpdateInventarioDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteInventario(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
