import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService, UsuarioDto, CreateUsuarioDto, UpdateUsuarioDto } from '../../Services/Usuario/usuario.service';

@Component({
  selector: 'app-usuario',
  imports: [CommonModule, FormsModule],
  templateUrl: './usuario.component.html',
})
export class UsuarioComponent implements OnInit {

  usuarios: UsuarioDto[] = [];
  contrasenaNueva: string = '';
  error = '';

  modalCrear = false;
  modalEditar = false;
  modalEliminar = false;
  modalDetalle = false;

  usuarioDetalle: UsuarioDto | null = null;
  usuarioEditar: UsuarioDto | null = null;
  usuarioEliminar: UsuarioDto | null = null;

  loadingCrear = false;
  errorCrear = '';
  errorEditar = '';

  roles = [
    { id: 1, nombre: 'Administrador' },
    { id: 2, nombre: 'Empleado' }
  ];

  nuevoUsuario: CreateUsuarioDto = this.usuarioVacio();

  constructor(private usuarioService: UsuarioService) {}

  ngOnInit() {
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.usuarioService.getUsuarios().subscribe({
      next: u => this.usuarios = u,
      error: () => this.error = 'Error al cargar usuarios.'
    });
  }

  private usuarioVacio(): CreateUsuarioDto {
    return {
      nombres: '',
      apellidos: '',
      email: '',
      contrasena: '',
      telefono: '',
      idRol: 2,
      idPais: 1,
      idCiudad: 1
    };
  }

  // ── Crear ──
  crearUsuario() {
    this.loadingCrear = true;
    this.errorCrear = '';
    this.usuarioService.createUsuario(this.nuevoUsuario).subscribe({
      next: () => {
        this.modalCrear = false;
        this.nuevoUsuario = this.usuarioVacio();
        this.loadingCrear = false;
        this.cargarUsuarios();
      },
      error: () => {
        this.errorCrear = 'No se pudo crear el usuario.';
        this.loadingCrear = false;
      }
    });
  }

  // ── Editar ──
  abrirModalEditar(u: UsuarioDto) {
    this.usuarioEditar = { ...u };
    this.errorEditar = '';
    this.contrasenaNueva = '';
    this.modalEditar = true;
  }

  guardarEdicion(u: UsuarioDto) {
    const dto: UpdateUsuarioDto = {
      nombres: u.nombres,
      apellidos: u.apellidos,
      email: u.email,
      contrasena: '',           // el backend debería ignorar si está vacío
      contrasenaNueva: null,
      telefono: u.telefono,
      idRol: u.rol === 'Administrador' ? 1 : 2,
      idPais: 1,
      idCiudad: 1
    };
    this.usuarioService.updateUsuario(u.id, dto).subscribe({
      next: () => { this.modalEditar = false; this.cargarUsuarios(); },
      error: () => this.errorEditar = 'Error al actualizar usuario.'
    });
  }

  // ── Eliminar ──
  confirmarEliminar(id: number) {
    this.usuarioService.deleteUsuario(id).subscribe({
      next: () => {
        this.usuarios = this.usuarios.filter(u => u.id !== id);
        this.modalEliminar = false;
        this.usuarioEliminar = null;
      },
      error: () => this.error = 'Error al eliminar usuario.'
    });
  }
}