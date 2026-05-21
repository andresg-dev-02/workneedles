export interface ProductoModel {
    id: number;
    nombre: string;
    descripcion: string;
    material?: string;
    preciobase: number;
    urlimagen?: string;
    activo: boolean;
    fechacreacion?: string; 
    categoria: string;
    genero?: string;
}
