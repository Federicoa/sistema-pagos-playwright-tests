import { Page } from '@playwright/test';

export class LoginPage {

  constructor(private page: Page) {}

  async irAlLogin() {
    await this.page.goto('/Usuario/Login');
  }

  async login(email: string, password: string) {

    await this.page.getByRole('textbox', { name: 'Ingrese Mail' }).fill(email);

    await this.page.getByRole('textbox', { name: 'Ingrese Contraseña' }).fill(password);

    await this.page.getByRole('button', { name: 'Iniciar Sesión' }).click();

  }

  async logout() {

    await this.page.goto('/Usuario/Logout');

  }

}