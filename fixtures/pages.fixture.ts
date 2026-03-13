import { test as base } from '@playwright/test';
import { UsuarioPage } from '../pages/usuarios.page';
import { LoginPage } from '../pages/login.page';
import { PagosPage } from '../pages/pagos.page';
import { TipoGastoPage } from '../pages/tipos-gasto.page';

type Pages = {
  usuarioPage: UsuarioPage;
  loginPage: LoginPage;
  pagosPage: PagosPage;
  tipoGastoPage: TipoGastoPage;
};

export const test = base.extend<Pages>({
  usuarioPage: async ({ page }, use) => {
    const usuarioPage = new UsuarioPage(page);
    await use(usuarioPage);
  },

  loginPage: async ({ page }, use) => {
    const loginPage = new LoginPage(page);
    await use(loginPage);
  },

  pagosPage: async ({ page }, use) => {
    const pagosPage = new PagosPage(page);
    await use(pagosPage);
  },

  tipoGastoPage: async ({ page }, use) => {
  const tipoGastoPage = new TipoGastoPage(page);
  await use(tipoGastoPage);
},
});

export { expect } from '@playwright/test';