import sql from 'mssql';

const config: sql.config = {
  user: 'sa',
  password: 'Password123!',
  server: 'localhost',
  port: 1433,
  database: 'SistemaPagos',
  options: {
    trustServerCertificate: true
  }
};

export async function buscarPagoPorDescripcion(descripcion: string) {

  const pool = await sql.connect(config);

  const result = await pool.request()
    .input('descripcion', sql.VarChar, descripcion)
    .query(`
      SELECT *
      FROM dbo.Pago
      WHERE Descripcion = @descripcion
    `);

    //console.log('Resultado DB:', result.recordset[0]);

  return result.recordset[0];
}

export async function borrarPagoPorDescripcion(descripcion: string) {

  const pool = await sql.connect(config);

  await pool.request()
    .input('descripcion', sql.VarChar, descripcion)
    .query(`
      DELETE FROM dbo.Pago
      WHERE Descripcion = @descripcion
    `);
}

export async function buscarTipoGastoPorNombre(nombre: string) {

  const pool = await sql.connect(config);
  const result = await pool.request()
    .input('nombre', sql.VarChar,nombre)
    .query(`
      SELECT *
      FROM TiposDeGastos
      WHERE Nombre = @nombre
    `);

  return result.recordset[0];

}

export async function borrarTipoGastoPorNombre(nombre: string) {

  const pool = await sql.connect(config);

  await pool.request()
    .input('nombre', sql.VarChar, nombre)
    .query(`
      DELETE FROM dbo.TiposDeGastos
      WHERE Nombre = @nombre
    `);

}
export async function query(query: string) {
  const pool = await sql.connect(config);
  const result = await pool.request().query(query);
  return result.recordset;
}