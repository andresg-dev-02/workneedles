import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TallaDto, CreateTallaDto, TallasService } from '../../Services/Talla/tallas.service';

@Component({
  selector: 'app-tallas-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tallas-component.html',
})
export class TallasComponent implements OnInit {

  tallas: TallaDto[] = [];
  error = '';

  // Modal crear
  modalCrear = false;
  loadingCrear = false;
  errorCrear = '';
  nuevaTalla: CreateTallaDto = { nombre: '' };

  // Modal editar
  modalEditar = false;
  loadingEditar = false;
  tallaEditar: TallaDto | null = null;

  // Modal eliminar
  modalEliminar = false;
  loadingEliminar = false;
  tallaEliminar: TallaDto | null = null;

  constructor(private tallasService: TallasService) {}

  ngOnInit() { this.getTallas(); }

  getTallas() {
    this.tallasService.getTallas().subscribe({
      next: r => this.tallas = r,
      error: () => this.error = 'No se pudieron cargar las tallas.'
    });
  }

  // ── Crear ──
  crearTalla() {
    if (!this.nuevaTalla.nombre) return;
    this.loadingCrear = true;
    this.errorCrear = '';
    this.tallasService.createTalla(this.nuevaTalla).subscribe({
      next: () => {
        this.modalCrear = false;
        this.nuevaTalla = { nombre: '' };
        this.loadingCrear = false;
        this.getTallas();
      },
      error: () => { this.errorCrear = 'No se pudo crear la talla.'; this.loadingCrear = false; }
    });
  }

  // ── Editar ──
  abrirEditar(talla: TallaDto) {
    this.tallaEditar = { ...talla };
    this.modalEditar = true;
  }

  guardarEdicion() {
    if (!this.tallaEditar) return;
    this.loadingEditar = true;
    this.tallasService.updateTalla(this.tallaEditar.id, this.tallaEditar).subscribe({
      next: () => { this.modalEditar = false; this.loadingEditar = false; this.getTallas(); },
      error: () => { this.loadingEditar = false; }
    });
  }

  // ── Eliminar ──
  abrirEliminar(talla: TallaDto) {
    this.tallaEliminar = talla;
    this.modalEliminar = true;
  }

  confirmarEliminar() {
    if (!this.tallaEliminar) return;
    this.loadingEliminar = true;
    this.tallasService.deleteTalla(this.tallaEliminar.id).subscribe({
      next: () => {
        this.tallas = this.tallas.filter(t => t.id !== this.tallaEliminar!.id);
        this.modalEliminar = false;
        this.loadingEliminar = false;
      },
      error: () => { this.loadingEliminar = false; }
    });
  }
}