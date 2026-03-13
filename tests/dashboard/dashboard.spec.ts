import { test, expect } from '@playwright/test';

test('dashboard visible para usuario logueado', async ({ page }) => {

  await page.goto('/');

  await expect(
    page.getByText('Bienvenido al Sistema de Gestión de Pagos')
  ).toBeVisible();

});