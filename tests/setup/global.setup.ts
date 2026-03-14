import { chromium, FullConfig } from '@playwright/test';
import path from 'path';
import { LoginPage } from '../../pages/login.page';
import { users } from '../../data/users';
import { query } from '../../utils/db';

async function globalSetup(config: FullConfig) {

   // -------- SEED TIPOS DE GASTO --------
  await query(`
IF NOT EXISTS (SELECT 1 FROM TiposDeGastos WHERE Nombre = 'Comida')
INSERT INTO TiposDeGastos (Nombre, Descripcion)
VALUES ('Comida', 'Gastos de comida')

IF NOT EXISTS (SELECT 1 FROM TiposDeGastos WHERE Nombre = 'Transporte')
INSERT INTO TiposDeGastos (Nombre, Descripcion)
VALUES ('Transporte', 'Gastos de transporte')

IF NOT EXISTS (SELECT 1 FROM TiposDeGastos WHERE Nombre = 'Servicios')
INSERT INTO TiposDeGastos (Nombre, Descripcion)
VALUES ('Servicios', 'Pago de servicios')

IF NOT EXISTS (SELECT 1 FROM TiposDeGastos WHERE Nombre = 'Entretenimiento')
INSERT INTO TiposDeGastos (Nombre, Descripcion)
VALUES ('Entretenimiento', 'Gastos de ocio')
`);

  

  const browser = await chromium.launch();
  const baseURL = config.projects[0].use?.baseURL || 'http://localhost:8080';

  // ---------- GERENTE ----------
  const contextGerente = await browser.newContext();
  const pageGerente = await contextGerente.newPage();

  const loginGerente = new LoginPage(pageGerente);

  await pageGerente.goto(`${baseURL}/Usuario/Login`);
  await loginGerente.login(users.gerente.email, users.gerente.password);

  await pageGerente.waitForLoadState('networkidle');

  await contextGerente.storageState({
    path: path.resolve(__dirname, '../../playwright/.auth/gerente.json')
  });

  // ---------- EMPLEADO ----------
  const contextEmpleado = await browser.newContext();
  const pageEmpleado = await contextEmpleado.newPage();

  const loginEmpleado = new LoginPage(pageEmpleado);

  await pageEmpleado.goto(`${baseURL}/Usuario/Login`);
  await loginEmpleado.login(users.empleado.email, users.empleado.password);

  await pageEmpleado.waitForLoadState('networkidle');

  await contextEmpleado.storageState({
    path: path.resolve(__dirname, '../../playwright/.auth/empleado.json')
  });

  await browser.close();
}

export default globalSetup;