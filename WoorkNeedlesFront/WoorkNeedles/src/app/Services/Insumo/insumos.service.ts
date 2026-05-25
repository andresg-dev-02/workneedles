import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';


export interface InsumoDto {
  id: number;
  nombre: string;
  descripcion: string;
  unidadmedida: string;
  stockactual: number;
  stockalerta: number;
  precio: number;
  proveedor?: string | null;
  activo: boolean;
  categoria: string;
  fechacreacion: string | null;
  fechamodificacion: string | null;
}

export interface CreateInsumoDto {
  idcategoria: number;
  nombre: string;
  descripcion: string;
  unidadmedida: string;
  stockactual: number;
  stockalerta: number;
  precio: number;
  proveedor?: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class InsumosService
{
  private apiUrl = environment.apiUrl + '/Insumo';
  
  constructor(private http: HttpClient) {}

  getInsumos(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`);
  }

  createInsumo(dto: CreateInsumoDto): Observable<any> {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateInsumo(id: number, dto: InsumoDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteInsumo(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
