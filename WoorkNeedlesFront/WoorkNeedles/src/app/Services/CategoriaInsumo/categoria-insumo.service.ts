import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface CategoriaInsumoDto {
  id: number;
  nombre: string;
  descripcion: string;
}

export interface CreateCategoriaInsumoDto {
  nombre: string;
  descripcion: string;
}

export interface UpdateCategoriaInsumoDto {
  nombre: string;
  descripcion: string;
}

@Injectable({
  providedIn: 'root',
})
export class CategoriaInsumoService {
  private apiUrl = environment.apiUrl + '/CategoriaInsumo';

  constructor(private http: HttpClient) {}

  getCategoriasInsumo(): Observable<CategoriaInsumoDto[]> {
    return this.http.get<CategoriaInsumoDto[]>(`${this.apiUrl}`);
  }

  getCategoriaInsumoById(id: number): Observable<CategoriaInsumoDto> {
    return this.http.get<CategoriaInsumoDto>(`${this.apiUrl}/${id}`);
  }

  createCategoriaInsumo(dto: CreateCategoriaInsumoDto): Observable<any> {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateCategoriaInsumo(id: number, dto: UpdateCategoriaInsumoDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteCategoriaInsumo(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}