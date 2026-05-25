import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import id from '@angular/common/locales/extra/id';


export interface CategoriaProductoDto 
{
  id: number;
  nombre: string;
  descripcion: string;
  fechacreacion: string | null;
  fechamodificacion: string | null;
}

export interface CreateCategoriaProductoDto
{
  nombre: string;
  descripcion: string;
}

@Injectable({
  providedIn: 'root',
})
export class CategoriaProductoService
{
  private apiUrl = environment.apiUrl + '/CategoriaProducto';

  constructor(private http: HttpClient) {}

  getCategoriasProducto(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`);
  }

  createCategoriaProducto(dto: CreateCategoriaProductoDto): Observable<any> {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateCategoriaProducto(id: number, dto: CategoriaProductoDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteCategoriaProducto(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
