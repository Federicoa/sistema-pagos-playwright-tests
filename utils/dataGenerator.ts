export function generarUsuario() {

  const nombres = ['Juan', 'Maria', 'Pedro', 'Ana', 'Luis', 'Sofia'];
  const apellidos = ['Perez', 'Gomez', 'Rodriguez', 'Fernandez', 'Lopez'];

  const nombre = nombres[Math.floor(Math.random() * nombres.length)];
  const apellido = apellidos[Math.floor(Math.random() * apellidos.length)];

  return { nombre, apellido };
}

export function generarEmail(nombre: string, apellido: string) {

  

  return (
    nombre.substring(0, 3).toLowerCase() +
    apellido.substring(0, 3).toLowerCase() +
    '@laEmpresa.com'
  );
}