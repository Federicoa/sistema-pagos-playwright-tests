import { test as base } from '@playwright/test';
import { generarUsuario, generarEmail } from '../utils/dataGenerator';

type UserData = {
  nombre: string;
  apellido: string;
  email: string;
};

export const test = base.extend<{
  userData: UserData;
}>({

  userData: async ({}, use) => {

    const { nombre, apellido } = generarUsuario();
    const email = generarEmail(nombre, apellido);

    await use({
      nombre,
      apellido,
      email
    });

  }

});

export { expect } from '@playwright/test';