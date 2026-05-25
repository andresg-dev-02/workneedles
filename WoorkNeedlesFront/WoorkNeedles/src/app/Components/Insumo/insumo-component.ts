import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InsumosService, InsumoDto } from '../../Services/Insumo/insumos.service';

@Component({
  selector: 'app-insumo-component',
  imports: [CommonModule],
  templateUrl: './insumo-component.html',
  styleUrl: './insumo-component.css',
})
export class InsumosComponent implements OnInit {
  insumo: InsumoDto[] = [];

  constructor(private insumosService: InsumosService) {}

  ngOnInit() {
    this.getInsumos();
  }

  getInsumos() {
    this.insumosService.getInsumos().subscribe({
      next: (data: InsumoDto[]) => {
        this.insumo = data;
      }
    });
  }
}
