import { Page, expect } from '@playwright/test';

export class PagosPage {

  constructor(private page: Page) {}

  async irAltaPagoUnico() {
    await this.page.goto('/Usuario/AltaPagoUnico');
  }

  async verificarPaginaAltaPagoUnico() {
    await expect(
      this.page.getByRole('heading', { name: /Agregar Pago Único/i })
    ).toBeVisible();
  }

  async crearPagoUnico(descripcion: string, metodoPago: string, tipoGasto?: string) {

  await this.page.selectOption('[name="metodoDePago"]', metodoPago);

  if (tipoGasto) {
    await this.page.selectOption('[name="tipoDeGasto"]', { label: tipoGasto });
  } else {
    await this.page.selectOption('[name="tipoDeGasto"]', { index: 0 });
  }

  await this.page.fill('[name="descripcion"]', descripcion);

  await this.page.fill('[name="montoPago"]', '1500');

  const hoy = new Date().toISOString().split('T')[0];
  await this.page.fill('[name="fechaPago"]', hoy);

  await this.page.fill('[name="numeroRecibo"]', '123456');

  await Promise.all([
    this.page.waitForURL(/ListarTodosLosPagosDeUnUsuario/),
    this.page.getByRole('button', { name: 'Guardar Pago' }).click()
  ]);

}

  async irAltaPagoRecurrente() {
    await this.page.goto('/Usuario/AltaPagoRecurrente');
  }

  async verificarPaginaAltaPagoRecurente() {
    await expect(
      this.page.getByRole('heading', { name: /Agregar Pago Recurrente/i })
    ).toBeVisible();
  }

async crearPagoRecurrente(descripcion: string, metodoPago: string, fechaFin?: Date) {

  await this.page.selectOption('[name="metodoDePago"]', metodoPago);

  await this.page.selectOption('[name="tipoDeGasto"]', { index: 0 });

  await this.page.fill('[name="descripcion"]', descripcion);

  await this.page.fill('[name="montoPago"]', '1000');

  const hoy = new Date().toISOString().split('T')[0];
  await this.page.fill('[name="fechaInicio"]', hoy);

  if (fechaFin) {
    const fechaFinStr = fechaFin.toISOString().split('T')[0];
    await this.page.fill('[name="fechaFin"]', fechaFinStr);
  }
  

  await Promise.all([
    this.page.waitForURL(/ListarTodosLosPagosDeUnUsuario/),
    this.page.getByRole('button', { name: 'Guardar Pago' }).click()
  ]);

}

async crearPagoUnicoMenorACero(descripcion: string, metodoPago: string) {

  await this.page.selectOption('[name="metodoDePago"]', metodoPago);

  await this.page.selectOption('[name="tipoDeGasto"]', { index: 0 });

  await this.page.fill('[name="descripcion"]', descripcion);

  await this.page.fill('[name="montoPago"]', '-23');

  const hoy = new Date().toISOString().split('T')[0];
  await this.page.fill('[name="fechaPago"]', hoy);

  await this.page.fill('[name="numeroRecibo"]', '123456');


    this.page.getByRole('button', { name: 'Guardar Pago' }).click()
  

}

async crearPagoRecurrenteMenorACero(descripcion: string, metodoPago: string, fechaFin?: Date) {

  await this.page.selectOption('[name="metodoDePago"]', metodoPago);

  await this.page.selectOption('[name="tipoDeGasto"]', { index: 0 });

  await this.page.fill('[name="descripcion"]', descripcion);

  await this.page.fill('[name="montoPago"]', '-12');

  const hoy = new Date().toISOString().split('T')[0];
  await this.page.fill('[name="fechaInicio"]', hoy);

  if (fechaFin) {
    const fechaFinStr = fechaFin.toISOString().split('T')[0];
    await this.page.fill('[name="fechaFin"]', fechaFinStr);
  }
     this.page.getByRole('button', { name: 'Guardar Pago' }).click()
  

}

}