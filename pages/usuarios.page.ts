import { Page, expect } from '@playwright/test';


export class UsuarioPage {

  constructor(private page: Page) {}

  

  async irAltaUsuario() {
    await this.page.goto('/Usuario/AltaUsuario');
  }


  async verificarPaginaAltaUsuario() {
    await expect(
      this.page.getByRole('heading', { name: /Alta de Usuario/i })
    ).toBeVisible();
  }

  

  
async crearUsuario(nombre: string, apellido: string, password: string) {

  await this.page.fill('[name="nombre"]', nombre);
  await this.page.fill('[name="apellido"]', apellido);
  await this.page.fill('[name="contrasenia"]', password);
  const hoy = new Date().toISOString().split('T')[0];
  await this.page.fill('[name="fechaIncorporacionAEmpresa"]', hoy);
  await this.page.selectOption('[name="nombreEquipo"]', { index: 1 });
  await this.page.selectOption('[name="rol"]', { index: 1 });
  

  await Promise.all([
    this.page.waitForURL(/ListadoDeUsuarios/),
    this.page.getByRole('button', { name: 'Crear Usuario' }).click()
  ]);

}



}