import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ColoresService, ColorDto, CreateColorDto } from '../../Services/Color/colores.service';

@Component({
  selector: 'app-colores.component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './colores.component.html',
})
export class ColoresComponent implements OnInit {

  colores: ColorDto[] = [];
  error = '';

  // Modal crear
  modalCrear = false;
  loadingCrear = false;
  errorCrear = '';
  nuevoColor: CreateColorDto = { nombre: '', codigohex: '#000000' };

  // Modal editar
  modalEditar = false;
  loadingEditar = false;
  colorEditar: ColorDto | null = null;

  // Modal eliminar
  modalEliminar = false;
  loadingEliminar = false;
  colorEliminar: ColorDto | null = null;

  constructor(private coloresService: ColoresService) {}

  ngOnInit() { this.getColores(); }

  getColores() {
    this.coloresService.getColores().subscribe({
      next: r => this.colores = r,
      error: () => this.error = 'No se pudieron cargar los colores.'
    });
  }

  // ── Crear ──
  crearColor() {
    if (!this.nuevoColor.nombre || !this.nuevoColor.codigohex) return;
    this.loadingCrear = true;
    this.errorCrear = '';
    this.coloresService.createColor(this.nuevoColor).subscribe({
      next: () => {
        this.modalCrear = false;
        this.nuevoColor = { nombre: '', codigohex: '#000000' };
        this.loadingCrear = false;
        this.getColores();
      },
      error: () => { this.errorCrear = 'No se pudo crear el color.'; this.loadingCrear = false; }
    });
  }

  // ── Editar ──
  abrirEditar(color: ColorDto) {
    this.colorEditar = { ...color };
    this.modalEditar = true;
  }

  guardarEdicion() {
    if (!this.colorEditar) return;
    this.loadingEditar = true;
    this.coloresService.updateColor(this.colorEditar.id, {
      nombre: this.colorEditar.nombre,
      codigohex: this.colorEditar.codigohex
    }).subscribe({
      next: () => { this.modalEditar = false; this.loadingEditar = false; this.getColores(); },
      error: () => { this.loadingEditar = false; }
    });
  }

  // ── Eliminar ──
  abrirEliminar(color: ColorDto) {
    this.colorEliminar = color;
    this.modalEliminar = true;
  }

  confirmarEliminar() {
    if (!this.colorEliminar) return;
    this.loadingEliminar = true;
    this.coloresService.deleteColor(this.colorEliminar.id).subscribe({
      next: () => {
        this.colores = this.colores.filter(c => c.id !== this.colorEliminar!.id);
        this.modalEliminar = false;
        this.loadingEliminar = false;
      },
      error: () => { this.loadingEliminar = false; }
    });
  }
}