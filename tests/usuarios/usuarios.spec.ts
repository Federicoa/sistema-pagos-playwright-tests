import { test, expect } from '../../fixtures';

test.describe('Usuarios (solo gerente)', () => {

  test.beforeEach(({}, testInfo) => {
    test.skip(testInfo.project.name === 'chromium-empleado');
  });

  test('alta de usuario', async ({ gerentePage, usuarioPage, loginPage, userData }) => {

     // Arrange (preparación de datos y estado inicial)
    const password = 'password123';

    // Act (ejecución de las acciones del usuario)
    await usuarioPage.irAltaUsuario();
    await usuarioPage.verificarPaginaAltaUsuario();

    await usuarioPage.crearUsuario(
      userData.nombre,
      userData.apellido,
      password
    );

    //await loginPage.logout();
    await loginPage.irAlLogin();
    await loginPage.login(userData.email, password);

    // Assert (verificación del resultado esperado)
    await expect(
      gerentePage.getByRole('heading', {
        name: /Bienvenido al Sistema de Gestión de Pagos/i
      })
    ).toBeVisible();

  });

});
