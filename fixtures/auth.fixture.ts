import { test as base, Page } from '@playwright/test';
import { LoginPage } from '../pages/login.page';
import { users } from '../data/users';

type Fixtures = {
  gerentePage: Page;
  empleadoPage: Page;
};

export const test = base.extend<Fixtures>({
  gerentePage: async ({ page }, use) => {
    const loginPage = new LoginPage(page);

    await loginPage.irAlLogin();
    await loginPage.login(users.gerente.email, users.gerente.password);

    await use(page);
  },

  empleadoPage: async ({ page }, use) => {
    const loginPage = new LoginPage(page);

    await loginPage.irAlLogin();
    await loginPage.login(users.empleado.email, users.empleado.password);

    await use(page);
  }
});

export { expect } from '@playwright/test';