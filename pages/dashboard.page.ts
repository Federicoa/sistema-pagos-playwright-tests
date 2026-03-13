import { expect, Page } from '@playwright/test';

export class DashboardPage {

  constructor(private page: Page) {}

  async verificarDashboardGerente() {

    await expect(this.page.getByText('Bienvenido al Sistema de Gestión de Pagos')).toBeVisible();

    await expect(this.page.getByRole('link', { name: 'Alta de usuario' })).toBeVisible();

    await expect(this.page.getByRole('link', { name: 'Logout' })).toBeVisible();
  }

  async verificarDashboardEmpleado() {

    await expect(this.page.getByText('Bienvenido al Sistema de Gestión de Pagos')).toBeVisible();

    await expect(this.page.getByRole('link', { name: 'Alta de usuario' })).toHaveCount(0);

    await expect(this.page.getByRole('link', { name: 'Logout' })).toBeVisible();
  }
}