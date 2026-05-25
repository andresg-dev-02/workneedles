import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ColorDto {
  id: number;
  nombre: string;
  codigohex: string;
  fechacreacion: string | null;
  fechamodificacion: string | null;
}

export interface CreateColorDto {
  nombre: string;
  codigohex: string;
}

@Injectable({
  providedIn: 'root',
})
export class ColoresService {
  private apiUrl = environment.apiUrl + '/Color';

  constructor(private http: HttpClient) {}

  getColores(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`);
  }

  updateColor(id: number, dto: CreateColorDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteColor(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
