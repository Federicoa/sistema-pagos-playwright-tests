import { test, expect } from '../../fixtures';
import { buscarPagoPorDescripcion, borrarPagoPorDescripcion } from '../../utils/db';
import { generarDescripcionPago } from '../../utils/faker';

test('ir a alta de pago único', async ({ pagosPage }) => {

  // Arrange
  // No requiere datos previos

  // Act
  await pagosPage.irAltaPagoUnico();

  // Assert
  await pagosPage.verificarPaginaAltaPagoUnico();

});

test('crear pago único con crédito 10%', async ({ pagosPage, page }) => {

  // Arrange
  const descripcion = generarDescripcionPago('PagoCredito');
  

  try {

    // Act
    await pagosPage.irAltaPagoUnico();
    await pagosPage.crearPagoUnico(descripcion, 'CREDITO');

    // Assert (UI)
    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toBeVisible();
    await expect(filaPago).toContainText('CREDITO');
    await expect(filaPago).toContainText('1350');

    // Assert (DB)
    const pagoDB = await buscarPagoPorDescripcion(descripcion);
    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    // Cleanup
    await borrarPagoPorDescripcion(descripcion);

  }

});

test('crear pago único con débito 10%', async ({ pagosPage, page }) => {

  // Arrange
  const descripcion = generarDescripcionPago('PagoDebito');
  

  try {

    // Act
    await pagosPage.irAltaPagoUnico();
    await pagosPage.crearPagoUnico(descripcion, 'DEBITO');

    // Assert (UI)
    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toContainText('DEBITO');
    await expect(filaPago).toContainText('1350');

    // Assert (DB)
    const pagoDB = await buscarPagoPorDescripcion(descripcion);
    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    // Cleanup
    await borrarPagoPorDescripcion(descripcion);

  }

});

test('crear pago único con efectivo 20%', async ({ pagosPage, page }) => {

  // Arrange
  const descripcion = generarDescripcionPago('PagoEfectivo');
  

  try {

    // Act
    await pagosPage.irAltaPagoUnico();
    await pagosPage.crearPagoUnico(descripcion, 'EFECTIVO');

    // Assert (UI)
    await expect(page).toHaveURL(/ListarTodosLosPagosDeUnUsuario/);
    await expect(page.locator('table')).toBeVisible();

    const filaPago = page.locator('table tbody tr').filter({
      hasText: descripcion
    });

    await expect(filaPago).toContainText('EFECTIVO');
    await expect(filaPago).toContainText('1200');

    // Assert (DB)
    const pagoDB = await buscarPagoPorDescripcion(descripcion);
    expect(pagoDB).toBeTruthy();
    expect(pagoDB.Descripcion).toBe(descripcion);

  } finally {

    // Cleanup
    await borrarPagoPorDescripcion(descripcion);

  }

});

test('no permite crear pago único con monto negativo', async ({ pagosPage, page }) => {

  // Arrange
  const descripcion = generarDescripcionPago('PagoNegativoUnico');
  

  // Act
  await pagosPage.irAltaPagoUnico();
  await pagosPage.crearPagoUnicoMenorACero(descripcion, 'CREDITO');

  // Assert
  await expect(page).toHaveURL(/AltaPagoUnico/);
  await expect(page.getByText('El monto debe ser mayor a cero')).toBeVisible();

});