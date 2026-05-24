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
  modalDetalle = false;
  modalEditar = false;
  modalEliminar = false;
  clienteDetalle: ClienteDto | null = null;
  clienteEditar: ClienteDto | null = null;
  clienteEliminar: ClienteDto | null = null;
  error = '';

  constructor(private clienteService: ClienteService) {}

  ngOnInit() {
    this.clienteService.getClientes().subscribe({
      next: c => this.clientes = c,
      error: () => this.error = 'Error al cargar clientes.'
    });
   
  }

  guardarEdicion(cliente: ClienteDto) {
  // PUT /api/Cliente/{id}
    this.modalEditar = false;
  }

  onTipoClienteChange() {
    if (this.clienteEditar?.tipocliente === 'empresa') {
      this.clienteEditar.tipodocumento = 'nit';
    }
  }

  confirmarEliminar(id: number) {
    this.clienteService.deleteCliente(id).subscribe({
    next: () => {
      
      this.clientes = this.clientes.filter(c => c.id !== id);
      this.modalEliminar = false;
      this.clienteEliminar = null;
    },
    error: () => {
      this.error = 'Error al eliminar cliente.';
    }
  });
}

}
