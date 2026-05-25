import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TallaDto, TallasService } from '../../Services/Talla/tallas.service';

@Component({
  selector: 'app-tallas-component',
  imports: [CommonModule],
  templateUrl: './tallas-component.html',
  styleUrl: './tallas-component.css',
})
export class TallasComponent implements OnInit {
  tallas: TallaDto[] = [];

  constructor(private tallasService: TallasService) {}

  ngOnInit() {
    this.getTallas();
  }

  getTallas() {
    this.tallasService.getTallas().subscribe({
      next: (data: TallaDto[]) => {
        this.tallas = data;
      }
    });
  }
}
