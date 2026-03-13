using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Data;
using System.Linq;

namespace AplicacionWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly SistemaPagosContext _context;

        public UsuarioController(SistemaPagosContext context)
        {
            _context = context;
        }

        Sistema unS = Sistema.Instancia;
        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }
        public IActionResult ListadoDeUsuarios()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login");
            }
            int? rolInt = HttpContext.Session.GetInt32("rol");

            if (rolInt != null && (RolUsuario)rolInt == RolUsuario.GERENTE)
            {

                List<Usuario> Usuarios = new List<Usuario>();

                foreach (Usuario u in _context.Usuarios)
                {
                    Usuarios.Add(u);
                }
                if (Usuarios.Count == 0) 
                {
                    ViewBag.Info = "No hay usuarios registrados.";
                }
                else
                {
                    ViewBag.Usuarios = Usuarios;
                }
                
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }



        public IActionResult AltaUsuario()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login");
            }

            int? rolInt = HttpContext.Session.GetInt32("rol");
            if (rolInt == null || (RolUsuario)rolInt != RolUsuario.GERENTE)
            {
                return RedirectToAction("Index");
            }

            List<Equipo> equipos = new List<Equipo>();

            foreach (Equipo e in _context.Equipos)
            {
                equipos.Add(e);
            }

            ViewBag.Equipos = equipos; 
            return View();
        }

        [HttpPost]

        public IActionResult AltaUsuario(string nombre, string apellido, string contrasenia, string nombreEquipo, DateTime? fechaIncorporacionAEmpresa, string rol)
        {

            try
            {
                if (HttpContext.Session.GetString("email") == null)
                {
                    return RedirectToAction("Login");
                }
                int? rolInt = HttpContext.Session.GetInt32("rol");

                if (rolInt != null && (RolUsuario)rolInt == RolUsuario.GERENTE)
                {
                    Equipo? equipo = null;

                    foreach (Equipo e in _context.Equipos)
                    {
                        if (e.NombreEquipo == nombreEquipo)
                        {
                            equipo = e;
                        }
                    }

                    if (equipo == null)
                    {
                        ViewBag.Error = "El equipo no existe";
                        List<Equipo> equipos = new List<Equipo>();

                        foreach (Equipo e in _context.Equipos)
                        {
                            equipos.Add(e);
                        }

                        return View();
                    }
                    if (string.IsNullOrEmpty(rol))
                    {
                        ViewBag.Error = "Debe seleccionar un rol";
                        List<Equipo> equipos = new List<Equipo>();

                        foreach (Equipo e in _context.Equipos)
                        {
                            equipos.Add(e);
                        }

                        ViewBag.Equipos = equipos;
                        return View();
                    }
                    if (fechaIncorporacionAEmpresa == null)
                    {
                        ViewBag.Error = "Debe seleccionar una fecha";
                        List<Equipo> equipos = new List<Equipo>();

                        foreach (Equipo e in _context.Equipos)
                        {
                            equipos.Add(e);
                        }

                        ViewBag.Equipos = equipos;
                        return View();
                    }
                    RolUsuario rolEnum = Enum.Parse<RolUsuario>(rol);
                    Usuario nuevo = new Usuario(
                    nombre,
                    apellido,
                    contrasenia,
                    equipo,
                    fechaIncorporacionAEmpresa.Value,
                    rolEnum
                    );

                    _context.Usuarios.Add(nuevo);
                    _context.SaveChanges();
                    TempData["Mensaje"] = "Usuario creado exitosamente";
                    return RedirectToAction("ListadoDeUsuarios");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                List<Equipo> equipos = new List<Equipo>();

                foreach (Equipo e in _context.Equipos)
                {
                    equipos.Add(e);
                }

                ViewBag.Equipos = equipos;
                return View();
            }

        }

        public IActionResult AltaTipoDeGasto()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AltaTipoDeGasto(string nombre, string descripcion)
        {

            try
            {
                if (HttpContext.Session.GetString("email") == null)
                {
                    return RedirectToAction("Login");
                }
                int? rolInt = HttpContext.Session.GetInt32("rol");
                if (rolInt != null && (RolUsuario)rolInt == RolUsuario.GERENTE)
                {
                    TipoDeGasto tipo = new TipoDeGasto();
                    tipo.Nombre = nombre;
                    tipo.Descripcion = descripcion;

                    _context.TiposDeGastos.Add(tipo);
                    _context.SaveChanges();
                    TempData["Mensaje"] = "Tipo de gasto creado exitosamente";
                    return RedirectToAction("ListarTiposDeGasto");
                }
                else
                {
                    return RedirectToAction("Login");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

       



        public IActionResult ListarTodosLosUsuariosPorEquipo()
        {
            return View();
        }

        [HttpPost]

        public IActionResult ListarTodosLosUsuariosPorEquipo(string equipo)
        {

            try
            {

                if (HttpContext.Session.GetString("email") == null)
                {
                    return RedirectToAction("Login");
                }
                int? rolInt = HttpContext.Session.GetInt32("rol");
                if (rolInt != null && (RolUsuario)rolInt == RolUsuario.GERENTE)
                {

                    if (equipo != null)
                    {

                        List<Usuario> Usuarios = unS.ListarTodosLosUsuariosPorEquipo(equipo);
                        if (Usuarios.Count == 0)
                        {
                            ViewBag.Info = "No hay usuarios registrados en ese equipo.";
                        }
                        else
                        {
                            ViewBag.Usuarios = Usuarios;
                        }
                        return View();
                    }
                    else
                    {
                        ViewBag.Error = "Debe ingresar un nombre de equipo";
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(string email, string contrasenia)
        {


            try
            {

                Usuario? UnUsr = null;

                foreach (Usuario u in _context.Usuarios)
                {
                    if (u.Email.ToLower() == email.ToLower() && u.Contrasenia == contrasenia)
                    {
                        UnUsr = u;
                        break;
                    }
                }
                if (UnUsr == null)
                {
                    ViewBag.Error = "Email o contraseña incorrectos";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("email", UnUsr.Email);
                    HttpContext.Session.SetInt32("rol", (int)UnUsr.Rol);

                    TempData["Info"] = $"Bienvenido {UnUsr.NombreUsuario} {UnUsr.Apellido}";
                    return RedirectToAction("Index");

                }


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Info"] = "Sesión cerrada correctamente";
            return RedirectToAction("Index");
        }

        [HttpPost]
        
        public IActionResult EliminarTipoDeGasto(string nombre)
        {
            try
            {
                TipoDeGasto? tipo = null;

                foreach (TipoDeGasto t in _context.TiposDeGastos)
                {
                    if (t.Nombre == nombre)
                    {
                        tipo = t;
                        break;
                    }
                }

                if (tipo == null)
                {
                    TempData["Error"] = "El tipo de gasto no existe.";
                    return RedirectToAction("ListarTiposDeGasto");
                }

                // verificar si hay pagos usando ese tipo
                foreach (Pago p in _context.Set<Pago>())
                {
                    if (p.TipoDeGasto != null)
                    {
                        if (p.TipoDeGasto.Id == tipo.Id)
                        {
                            TempData["Error"] = "No se puede eliminar el tipo de gasto porque está asociado a pagos.";
                            return RedirectToAction("ListarTiposDeGasto");
                        }
                    }
                }

                _context.TiposDeGastos.Remove(tipo);
                _context.SaveChanges();

                TempData["Mensaje"] = "Tipo de gasto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("ListarTiposDeGasto");
        }
        public IActionResult ListarPagosEquipo(int? mesBuscado, int? anioBuscado)
        {
            try
            {
                if (HttpContext.Session.GetString("email") == null)
                {
                    return RedirectToAction("Login");
                }

                int? rolInt = HttpContext.Session.GetInt32("rol");

                if (rolInt != null && (RolUsuario)rolInt == RolUsuario.GERENTE)
                {
                    string? email = HttpContext.Session.GetString("email");

                    Usuario? UnUsr = null;

                    foreach (Usuario u in _context.Usuarios)
                    {
                        if (u.Email == email)
                        {
                            UnUsr = u;
                            break;
                        }
                    }

                    if (UnUsr == null)
                    {
                        return RedirectToAction("Login");
                    }

                    int mes = mesBuscado ?? DateTime.Now.Month;
                    int anio = anioBuscado ?? DateTime.Now.Year;

                    List<Pago> listaPagos = new List<Pago>();

                    foreach (Pago p in _context.Set<Pago>()
                            .Include("Usuario")
                            .Include("Usuario.MiEquipo")
                            .Include("TipoDeGasto"))
                    {
                        if (p.Usuario != null)
                        {
                            if (p.Usuario.MiEquipo != null)
                            {
                                if (p.Usuario.MiEquipo.Id == UnUsr.MiEquipo.Id)
                                {
                                    if (p.EsDelMes(mes, anio))
                                    {
                                        listaPagos.Add(p);
                                    }
                                }
                            }
                        }
                    }

                    listaPagos.Sort();

                    if (listaPagos.Count == 0)
                    {
                        ViewBag.Info = "No hay pagos registrados para el equipo en el mes y año seleccionados.";
                    }

                    ViewBag.Pagos = listaPagos;
                    ViewBag.Mes = mes;
                    ViewBag.Anio = anio;

                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult ListarTiposDeGasto()
        {
            try
            {
                string? email = HttpContext.Session.GetString("email");
                

                if (email == null)
                {
                    return RedirectToAction("Login");
                }
                int? rolInt = HttpContext.Session.GetInt32("rol");

                if (rolInt != null && (RolUsuario)rolInt == RolUsuario.GERENTE)
                {
                    List<TipoDeGasto> TiposDeGastos = new List<TipoDeGasto>();

                    foreach (TipoDeGasto t in _context.TiposDeGastos)
                    {
                        TiposDeGastos.Add(t);
                    }
                    if (TiposDeGastos.Count == 0)
                    {
                        ViewBag.Info = "No hay tipos de gasto registrados.";
                    }
                    else
                    {
                        ViewBag.TiposDeGastos = TiposDeGastos;
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }



        public IActionResult ListarTodosLosPagosDeUnUsuario()
        {
            try
            {
                string? email = HttpContext.Session.GetString("email");

                if (email == null)
                    return RedirectToAction("Login");

                Usuario? usuario = _context.Usuarios
                    .Include(u => u.MiEquipo)
                    .FirstOrDefault(u => u.Email == email);

                if (usuario == null)
                    return RedirectToAction("Login");

                List<Pago> pagos = new List<Pago>();

                foreach (Pago p in _context.Set<Pago>()
                    .Include(p => p.Usuario)
                    .Include(p => p.TipoDeGasto))
                {
                    if (p.Usuario.Email == email && p.CorrespondeAlMesActual())
                    {
                        pagos.Add(p);
                    }
                }

                pagos.Sort();

                ViewBag.Pagos = pagos;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult VerPerfil()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login");
            }

            int? rolInt = HttpContext.Session.GetInt32("rol");

            if (rolInt != null)
            {
                string? email = HttpContext.Session.GetString("email");

                Usuario? UnUsr = _context.Usuarios
                    .Include(u => u.MiEquipo)
                    .FirstOrDefault(u => u.Email == email);

                if (UnUsr == null)
                {
                    return RedirectToAction("Login");
                }

                ViewBag.Usuario = UnUsr;

                decimal total = 0;

                foreach (Pago p in _context.Set<Pago>()
                .Include("Usuario"))
                {
                    if (p.Usuario != null)
                    {
                        if (p.Usuario.Id == UnUsr.Id && p.CorrespondeAlMesActual())
                        {
                            total += p.CalcularMontoDelMes();
                        }
                    }
                }

                ViewBag.GastoMes = total;

                if (UnUsr.Rol == RolUsuario.GERENTE)
                {
                    List<Usuario> usuariosEquipo = new List<Usuario>();

                    foreach (Usuario u in _context.Usuarios)
                    {
                        if (u.MiEquipo != null && u.MiEquipo.Id == UnUsr.MiEquipo.Id && u.Id != UnUsr.Id)
                        {
                            usuariosEquipo.Add(u);
                        }
                    }

                    usuariosEquipo.Sort();

                    ViewBag.UsuariosDelEquipo = usuariosEquipo;
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult AltaPagoRecurrente()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login");
            }
            int? rolInt = HttpContext.Session.GetInt32("rol");

            if (rolInt != null)
            {
                List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                foreach (TipoDeGasto t in _context.TiposDeGastos)
                {
                    tipos.Add(t);
                }

                ViewBag.TiposGasto = tipos;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]

        public IActionResult AltaPagoRecurrente(MetodoDePago metodoDePago, string tipoDeGasto, string descripcion, decimal montoPago, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                if (HttpContext.Session.GetString("email") == null)
                {
                    return RedirectToAction("Login");
                }
                int? rolInt = HttpContext.Session.GetInt32("rol");

                if (rolInt != null)
                {
                    TipoDeGasto? tipoGasto = null;

                    foreach (TipoDeGasto t in _context.TiposDeGastos)
                    {
                        if (t.Nombre == tipoDeGasto)
                        {
                            tipoGasto = t;
                            break;
                        }
                    }
                    if (tipoGasto == null)
                    {
                        ViewBag.Error = "Tipo de gasto inválido.";
                        List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                        foreach (TipoDeGasto t in _context.TiposDeGastos)
                        {
                            tipos.Add(t);
                        }

                        ViewBag.TiposGasto = tipos;
                        return View();
                    }
                    string? email = HttpContext.Session.GetString("email");
                    Usuario? usuario = null;

                    foreach (Usuario u in _context.Usuarios)
                    {
                        if (u.Email == email)
                        {
                            usuario = u;
                            break;
                        }
                    }
                    if (usuario == null)
                    {
                        ViewBag.Error = "No se encontró el usuario en sesión.";
                        List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                        foreach (TipoDeGasto t in _context.TiposDeGastos)
                        {
                            tipos.Add(t);
                        }

                        ViewBag.TiposGasto = tipos;
                        return View();
                    }
                    if (fechaInicio == null)
                    {
                        ViewBag.Error = "La fecha de inicio es obligatoria.";
                        List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                        foreach (TipoDeGasto t in _context.TiposDeGastos)
                        {
                            tipos.Add(t);
                        }

                        ViewBag.TiposGasto = tipos;
                        return View();
                    }
                    PagoRecurrente pago = new PagoRecurrente(metodoDePago, tipoGasto, usuario, descripcion, montoPago, fechaInicio.Value, fechaFin);
                    pago.Validar();
                    _context.PagosRecurrentes.Add(pago);
                    _context.SaveChanges();
                    TempData["Mensaje"] = "Pago recurrente creado exitosamente";
                    return RedirectToAction("ListarTodosLosPagosDeUnUsuario");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                foreach (TipoDeGasto t in _context.TiposDeGastos)
                {
                    tipos.Add(t);
                }

                ViewBag.TiposGasto = tipos;
                return View();
            }
        }

        public IActionResult AltaPagoUnico()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login");
            }
            int? rolInt = HttpContext.Session.GetInt32("rol");

            if (rolInt != null)
            {
                List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                foreach (TipoDeGasto t in _context.TiposDeGastos)
                {
                    tipos.Add(t);
                }

                ViewBag.TiposGasto = tipos;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]

        public IActionResult AltaPagoUnico(MetodoDePago metodoDePago, string tipoDeGasto, string descripcion, decimal montoPago, DateTime? fechaPago, int? numeroRecibo, bool fueEnEfectivo)
        {

            try
            {
                if (HttpContext.Session.GetString("email") == null)
                {
                    return RedirectToAction("Login");
                }




                int? rolInt = HttpContext.Session.GetInt32("rol");

                if (rolInt != null)
                {

                    TipoDeGasto? tipoGasto = null;

                    foreach (TipoDeGasto t in _context.TiposDeGastos)
                    {
                        if (t.Nombre == tipoDeGasto)
                        {
                            tipoGasto = t;
                            break;
                        }
                    }
                    if (tipoGasto == null)
                    {
                        ViewBag.Error = "Tipo de gasto inválido.";
                        List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                        foreach (TipoDeGasto t in _context.TiposDeGastos)
                        {
                            tipos.Add(t);
                        }

                        ViewBag.TiposGasto = tipos;
                        return View();
                    }

                    string? email = HttpContext.Session.GetString("email");
                    Usuario? usuario = null;

                    foreach (Usuario u in _context.Usuarios)
                    {
                        if (u.Email == email)
                        {
                            usuario = u;
                            break;
                        }
                    }

                    if (usuario == null)
                    {
                        ViewBag.Error = "No se encontró el usuario en sesión.";
                        List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                        foreach (TipoDeGasto t in _context.TiposDeGastos)
                        {
                            tipos.Add(t);
                        }

                        ViewBag.TiposGasto = tipos;
                        return View();
                    }

                    if (fechaPago == null || numeroRecibo == null)
                    {
                        ViewBag.Error = "Campos vacíos. Por favor verificar";
                        List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                        foreach (TipoDeGasto t in _context.TiposDeGastos)
                        {
                            tipos.Add(t);
                        }

                        ViewBag.TiposGasto = tipos;
                        return View();
                    }
   

                    PagoUnico nuevoPago = new PagoUnico( metodoDePago,tipoGasto,usuario,descripcion, montoPago, fechaPago.Value, numeroRecibo.Value,fueEnEfectivo);
                    nuevoPago.Validar();
                    _context.PagosUnicos.Add(nuevoPago);
                    _context.SaveChanges();
                    TempData["Mensaje"] = "Pago creado exitosamente";
                    return RedirectToAction("ListarTodosLosPagosDeUnUsuario");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ViewBag.Error = ex.InnerException.Message;
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }

                List<TipoDeGasto> tipos = new List<TipoDeGasto>();

                foreach (TipoDeGasto t in _context.TiposDeGastos)
                {
                    tipos.Add(t);
                }

                ViewBag.TiposGasto = tipos;

                return View();
            }

        }


    }
}
