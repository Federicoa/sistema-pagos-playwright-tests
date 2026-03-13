import { faker } from '@faker-js/faker';

export function generarDescripcionPago(prefijo = 'Pago') {
  return `${prefijo}_${faker.string.alphanumeric(6)}`;
}

export function generarTipoGasto(prefijo = 'TipoGasto') {
  return `${prefijo}_${faker.string.alphanumeric(6)}`;
}