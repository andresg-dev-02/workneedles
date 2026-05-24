export interface PedidoModel {
  id: number;
  nombreCliente: string;
  nombreUsuario: string;
  fechapedido: string | null;
  fechentregaaprox: string;
  fechaentrega: string;
  fechamodificacion: string;
  estado: string;
  direccionentrega: string;
  observaciones: string;
  subtotal: number;
  descuento: number | null;
  total: number;
}