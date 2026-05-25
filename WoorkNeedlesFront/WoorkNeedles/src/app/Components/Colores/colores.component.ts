import { Component } from '@angular/core';
import { ColoresService, ColorDto, CreateColorDto } from '../../Services/Color/colores.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-colores.component',
  imports: [CommonModule],
  templateUrl: './colores.component.html',
})
export class ColoresComponent {
  colores: ColorDto[] = [];

  constructor(private coloresService: ColoresService) {}

  ngOnInit() {
    this.getColores();
  }

  getColores() {
    this.coloresService.getColores().subscribe({
      next: r => this.colores = r
    });
  }
}
