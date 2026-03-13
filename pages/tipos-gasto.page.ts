import { Page, expect } from '@playwright/test';

export class TipoGastoPage {

  constructor(private page: Page) {}

  async irListaTiposDeGasto() {
    await this.page.goto('/Usuario/ListarTiposDeGasto');
  }

  async irAltaTipoDeGasto() {
    await this.page.goto('/Usuario/AltaTipoDeGasto');
  }


  async verificarPaginaListaTiposDeGasto() {
    await expect(
      this.page.getByRole('heading', { name: /Lista de Tipo de Gastos/i })
    ).toBeVisible();
  }

  async verificarPaginaAltaTipoDeGasto() {
    await expect(
      this.page.getByRole('heading', { name: /Agregar Tipo de Gasto/i })
    ).toBeVisible();
  }

  
async crearTipoGasto(descripcion: string) {

  await this.page.fill('[name="nombre"]', descripcion);

  await this.page.fill('[name="descripcion"]', 'Descripción del tipo de gasto');

  await Promise.all([
    this.page.waitForURL(/ListarTiposDeGasto/),
    this.page.getByRole('button', { name: 'Guardar Tipo de Gasto' }).click()
  ]);

}


async eliminarTipoDeGasto(nombre: string) {

  const fila = this.page.locator('table tbody tr').filter({
    hasText: nombre
  });

  await fila.getByRole('button', { name: 'Eliminar' }).click();

}


}