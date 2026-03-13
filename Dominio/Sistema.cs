using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Pago> _pagos = new List<Pago>();
        private List<TipoDeGasto> _tiposDeGastos = new List<TipoDeGasto>();
        private List<Equipo> _equipos = new List<Equipo>();

        private static Sistema _instancia;

        private Sistema()
        {
            //Precarga();
        }

        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia;
            }
        }

        private void Precarga()
        {
            // ======== EQUIPOS ========
            _equipos.Add(new Equipo("Marketing"));
            _equipos.Add(new Equipo("Ventas"));
            _equipos.Add(new Equipo("TI"));
            _equipos.Add(new Equipo("Administracion"));

            // ======== TIPOS DE GASTO ========
            _tiposDeGastos.Add(new TipoDeGasto("Auto", "Gastos del vehículo: nafta, arreglos"));
            _tiposDeGastos.Add(new TipoDeGasto("Afters", "Gastos relacionados con salidas del equipo"));
            _tiposDeGastos.Add(new TipoDeGasto("Comida", "Almuerzos, cenas y viandas"));
            _tiposDeGastos.Add(new TipoDeGasto("Internet", "Servicios de internet y telefonía"));
            _tiposDeGastos.Add(new TipoDeGasto("Oficina", "Materiales y suministros de oficina"));
            _tiposDeGastos.Add(new TipoDeGasto("Eventos", "Eventos corporativos o reuniones"));
            _tiposDeGastos.Add(new TipoDeGasto("Capacitacion", "Cursos, talleres y capacitaciones"));
            _tiposDeGastos.Add(new TipoDeGasto("Viajes", "Gastos de transporte y alojamiento"));
            _tiposDeGastos.Add(new TipoDeGasto("Software", "Licencias de software y suscripciones"));
            _tiposDeGastos.Add(new TipoDeGasto("Regalos", "Regalos corporativos o a clientes"));

            // ======== USUARIOS ========
            _usuarios.Add(new Usuario("Federico", "Ameijenda", "Passw0rd1", _equipos[2], new DateTime(2020, 3, 10), RolUsuario.GERENTE));     // Equipo 2
            _usuarios.Add(new Usuario("María", "Lopez", "Segura2025", _equipos[0], new DateTime(2021, 7, 5), RolUsuario.GERENTE));            // Equipo 0
            _usuarios.Add(new Usuario("Juan", "Perez", "MiPass123", _equipos[1], new DateTime(2019, 11, 2), RolUsuario.GERENTE));             // Equipo 1
            _usuarios.Add(new Usuario("Ana", "Gomez", "ClaveSegura9", _equipos[3], new DateTime(2022, 1, 15), RolUsuario.GERENTE));           // Equipo 3
            _usuarios.Add(new Usuario("Carlos", "Diaz", "Diaz2025!", _equipos[2], new DateTime(2020, 6, 20), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Lucia", "Martinez", "Luz123456", _equipos[0], new DateTime(2021, 4, 30), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Sofia", "Rojas", "SofiPass10", _equipos[1], new DateTime(2022, 8, 12), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Diego", "Torres", "Dieg0Pass!", _equipos[3], new DateTime(2021, 2, 28), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Valentina", "Alvarez", "Valen2025$", _equipos[0], new DateTime(2020, 12, 1), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Matias", "Fernandez", "Mat1234567", _equipos[1], new DateTime(2019, 5, 18), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Camila", "Sanchez", "Cami98765", _equipos[2], new DateTime(2022, 6, 6), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Nicolas", "Vega", "NicoPass10", _equipos[3], new DateTime(2021, 9, 22), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Paula", "Ruiz", "Paula2025!", _equipos[0], new DateTime(2022, 3, 3), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Andres", "Castro", "Andr3sPass", _equipos[1], new DateTime(2020, 10, 10), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Florencia", "Molina", "FlorPass99", _equipos[2], new DateTime(2021, 11, 19), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Martin", "Ortiz", "M0rtiPass!", _equipos[3], new DateTime(2019, 8, 8), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Laura", "Herrera", "Laura12345", _equipos[0], new DateTime(2020, 2, 14), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Sebastian", "Lozano", "SebaPass77", _equipos[1], new DateTime(2022, 7, 7), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Camilo", "Paredes", "CamiloPass1", _equipos[2], new DateTime(2021, 1, 20), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Gabriela", "Cortes", "Gaby2025!", _equipos[3], new DateTime(2020, 5, 5), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Ricardo", "Ramirez", "RicaPass22", _equipos[0], new DateTime(2019, 12, 12), RolUsuario.EMPLEADO));
            _usuarios.Add(new Usuario("Juliana", "Suarez", "Juliana123", _equipos[1], new DateTime(2022, 9, 9), RolUsuario.EMPLEADO));



            // ======== PAGOS RECURRENTES CON LÍMITE (primeros 5 totalmente pagados) ========
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[0], _usuarios[0], "Recurrente total 1", 1000, new DateTime(2025, 1, 1), new DateTime(2025, 1, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.DEBITO, _tiposDeGastos[1], _usuarios[1], "Recurrente total 2", 1200, new DateTime(2025, 2, 1), new DateTime(2025, 2, 28)));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[2], _usuarios[2], "Recurrente total 3", 1500, new DateTime(2025, 3, 1), new DateTime(2025, 3, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[3], _usuarios[3], "Recurrente total 4", 1100, new DateTime(2025, 4, 1), new DateTime(2025, 4, 30)));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[4], _usuarios[4], "Recurrente total 5", 1300, new DateTime(2025, 5, 1), new DateTime(2025, 5, 31)));

            // ======== CON LÍMITE (≤5 cuotas) ========
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[3], _usuarios[8], "Recurrente ≤5 cuotas", 1200, new DateTime(2025, 1, 1), new DateTime(2025, 5, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[7], _usuarios[11], "Recurrente ≤5 cuotas", 1300, new DateTime(2025, 4, 1), new DateTime(2025, 8, 30)));
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[1], _usuarios[16], "Recurrente ≤5 cuotas", 1600, new DateTime(2025, 8, 1), new DateTime(2025, 12, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.DEBITO, _tiposDeGastos[5], _usuarios[20], "Recurrente ≤5 cuotas", 1200, new DateTime(2025, 2, 1), new DateTime(2025, 6, 30)));

            // ======== CON LÍMITE (6–9 cuotas) ========
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[2], _usuarios[7], "Recurrente 6-9 cuotas", 1500, new DateTime(2025, 1, 1), new DateTime(2025, 8, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[5], _usuarios[10], "Recurrente 6-9 cuotas", 1600, new DateTime(2025, 2, 1), new DateTime(2025, 9, 30)));
            AgregarPago(new PagoRecurrente(MetodoDePago.DEBITO, _tiposDeGastos[9], _usuarios[14], "Recurrente 6-9 cuotas", 1700, new DateTime(2025, 6, 1), new DateTime(2025, 12, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[3], _usuarios[11], "Recurrente 6-9 cuotas", 1750, new DateTime(2025, 10, 1), new DateTime(2025, 12, 31)));

            // ======== CON LÍMITE (>10 cuotas) ========
            AgregarPago(new PagoRecurrente(MetodoDePago.DEBITO, _tiposDeGastos[1], _usuarios[6], "Recurrente >10 cuotas", 2000, new DateTime(2025, 1, 1), new DateTime(2025, 12, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.DEBITO, _tiposDeGastos[6], _usuarios[11], "Recurrente >10 cuotas", 1400, new DateTime(2025, 3, 1), new DateTime(2025, 12, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[0], _usuarios[15], "Recurrente >10 cuotas", 1500, new DateTime(2025, 7, 1), new DateTime(2025, 12, 31)));
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[4], _usuarios[19], "Recurrente >10 cuotas", 2000, new DateTime(2025, 1, 1), new DateTime(2025, 12, 31)));

            // ======== PAGOS RECURRENTES SIN LÍMITE ========
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[0], _usuarios[5], "Recurrente sin límite", 1000, new DateTime(2025, 1, 1), null));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[4], _usuarios[9], "Recurrente sin límite efectivo", 1800, new DateTime(2025, 1, 1), null));
            AgregarPago(new PagoRecurrente(MetodoDePago.CREDITO, _tiposDeGastos[8], _usuarios[11], "Recurrente sin límite", 1101, new DateTime(2025, 5, 1), null));
            AgregarPago(new PagoRecurrente(MetodoDePago.DEBITO, _tiposDeGastos[2], _usuarios[17], "Recurrente sin límite efectivo", 1800, new DateTime(2025, 9, 1), null));
            AgregarPago(new PagoRecurrente(MetodoDePago.EFECTIVO, _tiposDeGastos[6], _usuarios[21], "Recurrente sin límite", 1300, new DateTime(2025, 3, 1), null));



            // ======== PAGOS ÚNICOS ========
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[0], _usuarios[0], "Único crédito 1", 100, new DateTime(2025, 10, 1), 1000, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[1], _usuarios[11], "Único efectivo 1", 200, new DateTime(2025, 11, 2), 1001, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[2], _usuarios[2], "Único crédito 2", 150, new DateTime(2025, 10, 3), 1002, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[3], _usuarios[3], "Único efectivo 2", 250, new DateTime(2025, 10, 4), 1003, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[4], _usuarios[4], "Único crédito 3", 300, new DateTime(2025, 10, 5), 1004, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[5], _usuarios[5], "Único efectivo 3", 180, new DateTime(2025, 10, 6), 1005, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[6], _usuarios[6], "Único crédito 4", 220, new DateTime(2025, 10, 7), 1006, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[7], _usuarios[7], "Único efectivo 4", 260, new DateTime(2025, 10, 8), 1007, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[8], _usuarios[8], "Único crédito 5", 140, new DateTime(2025, 10, 9), 1008, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[9], _usuarios[9], "Único efectivo 5", 240, new DateTime(2025, 10, 10), 1009, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[0], _usuarios[10], "Único crédito 6", 130, new DateTime(2025, 10, 11), 1010, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[1], _usuarios[11], "Único efectivo 6", 170, new DateTime(2025, 10, 12), 1011, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[2], _usuarios[12], "Único crédito 7", 190, new DateTime(2025, 10, 13), 1012, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[3], _usuarios[13], "Único efectivo 7", 210, new DateTime(2025, 10, 14), 1013, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[4], _usuarios[14], "Único crédito 8", 160, new DateTime(2025, 10, 15), 1014, false));
            AgregarPago(new PagoUnico(MetodoDePago.EFECTIVO, _tiposDeGastos[5], _usuarios[15], "Único efectivo 8", 200, new DateTime(2025, 10, 16), 1015, true));
            AgregarPago(new PagoUnico(MetodoDePago.CREDITO, _tiposDeGastos[6], _usuarios[16], "Único crédito 9", 150, new DateTime(2025, 10, 17), 1016, false));
        }

        



        public void ExisteUsuario(Usuario usuario)
        {
            if (_usuarios.Contains(usuario)) throw new Exception("El usuario ya existe en el sistema");
        }

        public void ExisteTipoDeGasto(TipoDeGasto tipoDeGasto)
        {
            if (_tiposDeGastos.Contains(tipoDeGasto)) throw new Exception("El tipo de gasto ya existe en el sistema");
        }

        public bool ExisteTipoDeGastoBool(TipoDeGasto tipoDeGasto)
        {
            return _tiposDeGastos.Contains(tipoDeGasto);
        }

        public List<Equipo> ListarTodosLosEquipos()
        {
            return _equipos;
        }



        public void AgregarUsuario(string nombreUsuario, string apellido, string contrasenia, Equipo miEquipo, DateTime fechaIncorporacionAEmpresa, RolUsuario rol)
        {
            try
            {
                Usuario nuevoUsuario = new Usuario(nombreUsuario, apellido, contrasenia, miEquipo, fechaIncorporacionAEmpresa, rol);
                string emailBase = nuevoUsuario.Email;
                string emailFinal = emailBase;
                int numeroMail = 1; // el incremental que hará único a cada mail

                // Separar nombre y dominio
                string[] partes = emailBase.Split('@');
                string nombreBase = partes[0];
                string dominio = partes[1];

                foreach (Usuario unU in _usuarios)
                {
                    if (unU.ExisteMail(emailFinal))
                    {
                        emailFinal = nombreBase + numeroMail + "@" + dominio;
                        numeroMail++;
                    }
                }

                nuevoUsuario.Email = emailFinal;

                nuevoUsuario.Validar();
                ExisteUsuario(nuevoUsuario);
                _usuarios.Add(nuevoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarPago(Pago unPago)
        { 
        if (unPago != null)
            {
                unPago.Validar();
                _pagos.Add(unPago);
            }
        }

     
        public void AgregarTipoDeGasto(string nombre, string descripcion)
        {
            try
            {
                TipoDeGasto nuevoTipoDeGasto = new TipoDeGasto(nombre, descripcion);
                nuevoTipoDeGasto.Validar();
                ExisteTipoDeGasto(nuevoTipoDeGasto);
                _tiposDeGastos.Add(nuevoTipoDeGasto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ListarTodosLosUsuarios()
        {
            return _usuarios;
        }

        public void EliminarTipoDeGasto(TipoDeGasto tipoDeGasto)
        {

            if (ExisteTipoDeGastoBool(tipoDeGasto))
            {
                foreach (Pago unP in _pagos)
                {
                    if (unP.ExistePagoConEseTipoDeGasto(tipoDeGasto))
                    {
                        throw new Exception("El tipo de gasto es usado para uno o mas pagos");
                    }
                }
                _tiposDeGastos.Remove(tipoDeGasto);
            }
            else
            {
                throw new Exception("El tipo de gasto no existe en el sistema");
            }
        }




        public List<Pago> ListarTodosLosPagosDeUnUsuario(string email)
        {
            List<Pago> aux = new List<Pago>();
            Usuario? usuario = DevolverUsuarioPorEmail(email);


            if (usuario != null)
            {
                foreach (Pago unP in _pagos)
                {
                    if (unP.EsDeUsuario(usuario)) aux.Add(unP);
                }
            }
            else
            {
                throw new Exception("No existe usuario con el mail ingresado");
            }

            if (aux.Count == 0) throw new Exception("El usuario no tiene pagos realizados");

            aux.Sort();

            return aux;
        }

        public List<Pago> ListarPagosDelMesActual(string email)
        {
            List<Pago> aux = new List<Pago>();
            Usuario? usuario = DevolverUsuarioPorEmail(email);

            if (usuario != null)
            {
                foreach (Pago unPago in _pagos)
                {
                    if (unPago.EsDeUsuario(usuario) && unPago.CorrespondeAlMesActual())
                    {
                        aux.Add(unPago);
                    }
                }
            }
            else
            {
                throw new Exception("No existe usuario con el mail ingresado");
            }

            if (aux.Count == 0)
                throw new Exception("El usuario no tiene pagos registrados para este mes");


            aux.Sort();

            return aux;

        }




        public decimal CalcularGastosDelMes(Usuario usuario)
        {
            decimal total = 0;

            foreach (Pago unPago in _pagos)
            {
                if (unPago.EsDeUsuario(usuario) && unPago.CorrespondeAlMesActual())
                {
                    total += unPago.CalcularMontoDelMes();
                }
            }
            return total;
        }


        public List<Usuario> ListarTodosLosUsuariosPorEquipo(string nombreEquipo)
        {
            List<Usuario> aux = new List<Usuario>();

            if (nombreEquipo != null)
            {
                foreach (Usuario unU in _usuarios)
                {
                    if (unU.EsDelEquipo(nombreEquipo)) aux.Add(unU);
                }
            }


            if (aux.Count == 0) throw new Exception("No existe equipo ingresado. Recuerde que nuestros equipos son Marketing, Ventas, TI y Administración");



            return aux;
        }

        public List<Usuario> ListarUsuarioDeUnEquipo(Usuario usuario)
        {
            Equipo equipoUsuario = usuario.MiEquipo;
            List<Usuario> aux = new List<Usuario>();
            if (usuario != null)
            {
                foreach (Usuario unU in _usuarios)
                {
                    if (unU.PerteneceAlMismoEquipo(equipoUsuario) && !unU.EsMismoUsuario(usuario)) aux.Add(unU);
                }
            }
            aux.Sort();
            return aux;
        }


        public Usuario? DevolverUsuarioPorEmail(string email)
        {
            foreach (Usuario unU in _usuarios)
            {
                if (unU.ExisteMail(email))
                {
                    return unU;
                }
            }
            return null;
        }

        public Equipo? DevolverEquipoPorNombre(string nombreEquipo)
        {
            // utilizado en Program al crear usuario, devuelve el equipo si existe
            foreach (Equipo unE in _equipos)
            {
                if (unE.EsNombre(nombreEquipo))
                {
                    return unE;
                }
            }
            return null;
        }

        public Usuario? DevolverUsuarioPorEmailYContrasenia(string email, string contrasenia)
        {
            foreach (Usuario unU in _usuarios)
            {
                if (unU.EsMailYContrasenia(email, contrasenia))
                {
                    return unU;
                }
            }
            return null;
        }

     



        public List<TipoDeGasto> ListarTiposDeGasto()
        {
            return _tiposDeGastos;
        }

        public TipoDeGasto? DevolverTipoDeGastoPorNombre(string nombre)
        {
            foreach (TipoDeGasto unT in _tiposDeGastos)
            {
                if (unT.EsNombreDeTipoDeGasto(nombre))
                {
                    return unT;
                }
            }
            return null;
        }

        public List<Pago> ListarPagosPorEquipo(Usuario usuario, int mesBuscado, int anioBuscado)
        {

            Equipo equipoGerente = usuario.MiEquipo;

            List<Pago> aux = new List<Pago>();

            foreach (Usuario unU in _usuarios)
            {
                if (unU.PerteneceAlMismoEquipo(equipoGerente) && !unU.EsMismoUsuario(usuario))

                {
                    foreach (Pago unP in _pagos)
                    {
                        if (unP.EsDeUsuario(unU) && unP.EsDelMes(mesBuscado, anioBuscado))
                        {
                            aux.Add(unP);
                        }
                    }                    
                }
            }
            aux.Sort();
            return aux;
        }
    }
}
