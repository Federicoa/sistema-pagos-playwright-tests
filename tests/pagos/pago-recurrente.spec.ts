import { test, expect } from '../../fixtures';
import { buscarPagoPorDescripcion, borrarPagoPorDescripcion } from '../../utils/db';
import { generarDescripcionPago } from '../../utils/faker';

test('ir a alta de pago recurrente', async ({ pagosPage }) => {

  

  await pagosPage.irAltaPagoRecurrente();
  await pagosPage.verificarPaginaAltaPagoRecurente();

});

test('crear pago recurrente sin limite', async ({ pagosPage, page }) => {

  
  const descripcion = generarDescripcionPago('PagoRecurrenteSinLimite');

  try {

    await pagosPage.irAltaPagoRecurrente();
    await pagosPage.crearPagoRecurrente(descripcion, 'CREDITO');

    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toBeVisible();
    await expect(filaPago).toContainText('CREDITO');
    await expect(filaPago).toContainText('1030');
    const pagoDB = await buscarPagoPorDescripcion(descripcion);

    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    await borrarPagoPorDescripcion(descripcion);

  }

});


test('crear pago recurrente con menos de 5 cuotas', async ({ pagosPage,page }) => {

  const descripcion = generarDescripcionPago('PagoRecurrenteCLimiteCincoCuotas');
  
  const fechaFin = new Date();
fechaFin.setMonth(fechaFin.getMonth() + 3);
  

  try {

    await pagosPage.irAltaPagoRecurrente();
    await pagosPage.crearPagoRecurrente(descripcion, 'CREDITO', fechaFin);

    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toBeVisible();
    await expect(filaPago).toContainText('CREDITO');
    await expect(filaPago).toContainText('1030');
    const pagoDB = await buscarPagoPorDescripcion(descripcion);

    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    await borrarPagoPorDescripcion(descripcion);

  }
});
  test('crear pago recurrente entre 6 y 9 cuotas', async ({ pagosPage,page }) => {

  const descripcion = generarDescripcionPago('PagoRecurrenteCLimiteSieteCuotas');
  
  const fechaFin = new Date();
fechaFin.setMonth(fechaFin.getMonth() + 7);
  

  try {

    await pagosPage.irAltaPagoRecurrente();
    await pagosPage.crearPagoRecurrente(descripcion, 'CREDITO', fechaFin);

    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toBeVisible();
    await expect(filaPago).toContainText('CREDITO');
    await expect(filaPago).toContainText('1050');
    const pagoDB = await buscarPagoPorDescripcion(descripcion);

    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    await borrarPagoPorDescripcion(descripcion);

  }
});
  test('crear pago recurrente con mas de 10 cuotas', async ({ pagosPage,page }) => {

  const descripcion = generarDescripcionPago('PagoRecurrenteCLimiteDoceCuotas');
  const fechaFin = new Date();
fechaFin.setMonth(fechaFin.getMonth() + 12);
  

  try {

    await pagosPage.irAltaPagoRecurrente();
    await pagosPage.crearPagoRecurrente(descripcion, 'CREDITO', fechaFin);

    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toBeVisible();
    await expect(filaPago).toContainText('CREDITO');
    await expect(filaPago).toContainText('1100');
    const pagoDB = await buscarPagoPorDescripcion(descripcion);

    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    await borrarPagoPorDescripcion(descripcion);

  }

});

test('no permite crear pago recurrente con monto negativo', async ({ pagosPage,page }) => {

  const descripcion = generarDescripcionPago('PagoRecurrenteNegativo');
  const fechaFin = new Date();
fechaFin.setMonth(fechaFin.getMonth() + 3);
  

  

    await pagosPage.irAltaPagoRecurrente();
    await pagosPage.crearPagoRecurrenteMenorACero(descripcion, 'CREDITO', fechaFin);
    await expect(page).toHaveURL(/AltaPagoRecurrente/);
    await expect(page.getByText('El monto debe ser mayor a cero')).toBeVisible();
    
});