import { test } from '@playwright/test';
import { LoginPage } from '../../pages/login.page';
import { DashboardPage } from '../../pages/dashboard.page';
import { users } from '../../data/users';

test.describe('Login', () => {
test('login rol Gerente', async ({ page }) => {

    const loginPage = new LoginPage(page);
    const dashboardPage = new DashboardPage(page);

  await loginPage.irAlLogin();

  await loginPage.login(users.gerente.email, users.gerente.password);

  await dashboardPage.verificarDashboardGerente();

});


test('login rol Empleado', async ({ page }) => {

    const loginPage = new LoginPage(page);
    const dashboardPage = new DashboardPage(page);

  await loginPage.irAlLogin();

  await loginPage.login(users.empleado.email, users.empleado.password);

  await dashboardPage.verificarDashboardEmpleado();

});


});

