import { test, expect } from '../../fixtures';
import { buscarTipoGastoPorNombre, borrarTipoGastoPorNombre } from '../../utils/db';
import { generarDescripcionPago, generarTipoGasto } from '../../utils/faker';

test.describe.serial('Tipos de gasto (solo gerente)', () => {

  // Saltea todo el bloque cuando el proyecto es empleado
  test.beforeEach(({}, testInfo) => {
    test.skip(testInfo.project.name === 'chromium-empleado');
  });

  test('ir a alta de tipo de gasto', async ({ tipoGastoPage }) => {

    // Arrange
    // No requiere preparación previa

    // Act
    await tipoGastoPage.irAltaTipoDeGasto();

    // Assert
    await tipoGastoPage.verificarPaginaAltaTipoDeGasto();

  });

  test('crear tipo de gasto', async ({ tipoGastoPage, gerentePage }) => {

    // Arrange
    const descripcion = generarTipoGasto('TipoGasto');

    try {

      // Act
      await tipoGastoPage.irAltaTipoDeGasto();
      await tipoGastoPage.crearTipoGasto(descripcion);

      // Assert
      await expect(gerentePage.locator('.alert-success'))
        .toContainText('Tipo de gasto creado exitosamente');

      await expect(gerentePage.locator('table'))
        .toContainText(descripcion);

    } finally {

      // Cleanup
      await borrarTipoGastoPorNombre(descripcion);

    }

  });

  test('eliminar tipo de gasto', async ({ tipoGastoPage, gerentePage }) => {

    // Arrange
    const descripcion = generarTipoGasto('TipoGastoEliminado');
    

    await tipoGastoPage.irAltaTipoDeGasto();
    await tipoGastoPage.crearTipoGasto(descripcion);

    await expect(gerentePage.locator('.alert-success'))
      .toContainText('Tipo de gasto creado exitosamente');

    await expect(gerentePage.locator('table'))
      .toContainText(descripcion);

    const tipoDB = await buscarTipoGastoPorNombre(descripcion);
    expect(tipoDB).toBeTruthy();
    expect(tipoDB.Nombre).toBe(descripcion);

    // Act
    await tipoGastoPage.eliminarTipoDeGasto(descripcion);

    // Assert
    await expect(gerentePage.locator('.alert-success'))
      .toContainText('Tipo de gasto eliminado correctamente');

    await expect(gerentePage.locator('table'))
      .not.toContainText(descripcion);

    const tipoDBEliminado = await buscarTipoGastoPorNombre(descripcion);
    expect(tipoDBEliminado).toBeFalsy();

  });

  test('eliminar tipo de gasto asociado a un pago', async ({ tipoGastoPage, pagosPage, gerentePage }) => {

    // Arrange
    const descripcion = generarTipoGasto('TipoGastoAsociado');
    const descripcionPago = generarDescripcionPago('PagoAsociado');

    await tipoGastoPage.irAltaTipoDeGasto();
    await tipoGastoPage.crearTipoGasto(descripcion);

    await expect(gerentePage.locator('.alert-success'))
      .toContainText('Tipo de gasto creado exitosamente');

    await expect(gerentePage.locator('table'))
      .toContainText(descripcion);

    const tipoDB = await buscarTipoGastoPorNombre(descripcion);
    expect(tipoDB).toBeTruthy();
    expect(tipoDB.Nombre).toBe(descripcion);

    await pagosPage.irAltaPagoUnico();
    await pagosPage.crearPagoUnico(descripcionPago, 'CREDITO', descripcion);

    await tipoGastoPage.irListaTiposDeGasto();
    console.log(await gerentePage.url());
    await tipoGastoPage.verificarPaginaListaTiposDeGasto();

    // Act
    await tipoGastoPage.eliminarTipoDeGasto(descripcion);

    // Assert
    await expect(gerentePage.locator('.alert'))
      .toContainText('No se puede eliminar el tipo de gasto porque está asociado a pagos');

  });

});