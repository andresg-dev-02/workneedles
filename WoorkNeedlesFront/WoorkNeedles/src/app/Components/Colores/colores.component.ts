import { Component } from '@angular/core';
import { ColoresService, ColorDto, CreateColorDto } from '../../Services/Color/colores.service';

@Component({
  selector: 'app-colores.component',
  imports: [],
  templateUrl: './colores.component.html',
})
export class ColoresComponent {
  colores: ColorDto[] = [];

  constructor(private coloresService: ColoresService) {}

  ngOnInit() {
    this.getColores();
  }

  getColores() {
    this.coloresService.getColores().subscribe((data) => {
      this.colores = data;
    });
  }
}
