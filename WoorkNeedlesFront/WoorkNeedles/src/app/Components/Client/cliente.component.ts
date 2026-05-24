import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClienteService, ClienteDto } from '../../Services/Client/cliente.service';

@Component({
  selector: 'app-cliente.component',
  imports: [CommonModule, FormsModule],
  templateUrl: './cliente.component.html',
})
export class ClienteComponent implements OnInit {

  clientes: ClienteDto[] = [];
  modalCrear = false;
  modalDetalle = false;
  modalEditar = false;
  modalEliminar = false;
  clienteDetalle: ClienteDto | null = null;
  clienteEditar: ClienteDto | null = null;
  clienteEliminar: ClienteDto | null = null;
  ubication: any = null; 
  error = '';

  // Crear cliente
  loadingCrear = false;
  errorCrear = '';
  nuevoCliente = this.clienteVacio();

  constructor(private clienteService: ClienteService) {}

  ngOnInit() {
    this.cargarClientes();
  }
cargarClientes() {

  this.clienteService.getClientes().subscribe({
    next: c => this.clientes = c,
    error: () => this.error = 'Error al cargar clientes.'
  });

  this.clienteService.getUbicacion().subscribe({
    next: u => {
      this.ubication = u;

      console.log(this.ubication);
    },
    error: () => {}
  });

}

 
  // ── Crear ──
  private clienteVacio() {
    return {
      tipocliente: 'persona',
      tipodocumento: 'cedula',
      documento: '',
      nombres: '',
      apellidos: '',
      razonsocial: '',
      email: '',
      telefono: '',
      direccion: '',
      idPais: 1,
      idDepart: 1,
      idCiudad: 1
    };
  }

  onNuevoTipoClienteChange() {
    if (this.nuevoCliente.tipocliente === 'empresa') {
      this.nuevoCliente.tipodocumento = 'nit';
    }
  }

  crearCliente() {
    this.loadingCrear = true;
    this.errorCrear = '';

    this.clienteService.createCliente(this.nuevoCliente).subscribe({
      next: () => {
        this.modalCrear = false;
        this.nuevoCliente = this.clienteVacio();
        this.loadingCrear = false;
        this.cargarClientes();
      },
      error: () => {
        this.errorCrear = 'No se pudo crear el cliente.';
        this.loadingCrear = false;
      }
    });
  }

  // ── Editar ──
  onTipoClienteChange() {
    if (this.clienteEditar?.tipocliente === 'empresa') {
      this.clienteEditar.tipodocumento = 'nit';
    }
  }

  guardarEdicion(cliente: ClienteDto) {
    this.clienteService.updateCliente(cliente.id, cliente).subscribe({
      next: () => {
        this.modalEditar = false;
        this.cargarClientes();
      },
      error: () => this.error = 'Error al actualizar cliente.'
    });
  }

  // ── Eliminar ──
  confirmarEliminar(id: number) {
    this.clienteService.deleteCliente(id).subscribe({
      next: () => {
        this.clientes = this.clientes.filter(c => c.id !== id);
        this.modalEliminar = false;
        this.clienteEliminar = null;
      },
      error: () => this.error = 'Error al eliminar cliente.'
    });
  }
}