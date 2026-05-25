import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventarioService, InventarioDto } from '../../Services/Inventario/inventarios.service';

@Component({
  selector: 'app-inventario-component',
  imports: [CommonModule],
  templateUrl: './inventario-component.html',
  styleUrl: './inventario-component.css',
})
export class InventarioComponent implements OnInit 
  {
      inventario: InventarioDto[] = [];


  constructor(private inventarioService: InventarioService) {}
  
  
  ngOnInit() {
    this.getInventarios();
  }

  getInventarios() {
      this.inventarioService.getInventarios().subscribe({
        next: (data: InventarioDto[]) => {
          this.inventario = data;
        }
      });
  }

  

}