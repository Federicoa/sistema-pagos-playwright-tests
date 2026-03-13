using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio

{
    public enum RolUsuario
    {
        EMPLEADO = 0,
        GERENTE = 1
    }
    public class Usuario : IComparable<Usuario>
    {
        public RolUsuario Rol { get; set; }
        string _nombreUsuario;
        string _apellido;
        string _contrasenia;
        string _email;
        Equipo _miEquipo;
        DateTime _fechaIncorporacionAEmpresa;

        public int Id { get; set; }
        public Equipo MiEquipo { get => _miEquipo; set => _miEquipo = value; }
        public string Email { get => _email; set => _email = value; }

        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Contrasenia { get => _contrasenia; set => _contrasenia = value; }
        
        public DateTime FechaIncorporacionAEmpresa { get => _fechaIncorporacionAEmpresa; set => _fechaIncorporacionAEmpresa = value; }

        public Usuario()
        {
        }


        public Usuario(string nombreUsuario, string apellido, string contrasenia, Equipo _miEquipo, DateTime fechaIncorporacionAEmpresa, RolUsuario rol)
        {
            _nombreUsuario = nombreUsuario;
            _apellido = apellido;
            _contrasenia = contrasenia;
            MiEquipo = _miEquipo;
            _fechaIncorporacionAEmpresa = fechaIncorporacionAEmpresa;

            
            Rol  = rol;
            //validamos antes de generar mail
            Validar();
            Email = GenerarEmail(_nombreUsuario, _apellido);
        }

        private void ValidarCamposVacios()
        {
            if (string.IsNullOrEmpty(_nombreUsuario)) throw new Exception("Nombre no puede ser vacio");
            if (string.IsNullOrEmpty(_apellido)) throw new Exception("Apellido no puede ser vacio");
            if (string.IsNullOrEmpty(_contrasenia)) throw new Exception("Contraseña no puede ser vacia");
        }

        private void ValidarContrasenia()
        {
            if (_contrasenia == null)
            {
                throw new Exception("La contraseña no puede ser vacía");
            }
            if (_contrasenia.Length < 8)
            {
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");
            }
            if(FechaIncorporacionAEmpresa > DateTime.Now)
            {
                throw new Exception("La fecha de incorporaciòn debe ser anterior o igual a la fecha actual.");
            }
        }

        public void Validar()
        {
            ValidarCamposVacios();
            ValidarContrasenia();
        }

        private string GenerarEmail(string _nombre, string _apellido)
        {
            

            // Recorrer las primeras 3 letras del nombre y las primeras 3 letras del apellido y las concatena
            string email = "";
            for (int i = 0; i < _nombre.Length; i++)
            {
                if (i < 3)
                {
                    email += _nombre[i];
                }

            }
            for (int i = 0; i < _apellido.Length; i++)
            {
                if (i < 3)
                {
                    email += _apellido[i];
                }

            }
            email = email.ToLower();
            email += "@laEmpresa.com";

            return email;

        }

        public bool ExisteMail(string email)
        {
            return Email == email;
        }

        public bool EsDelEquipo(string nombreEquipo)
        {
            // utilizada en el metodo ListarUsuariosPorEquipo del sistema
            return MiEquipo.EsNombre(nombreEquipo);
        }

        public string MostrarNombreEmail()
        {
            // utilizada en el metodo ListarTodosLosUsuariosPorEquipo del program
            return $"Nombre: {_nombreUsuario} - Email: {Email}";
        }

        public bool EsMismoUsuario(Usuario otroUsuario)
        {
            return this.Email == otroUsuario.Email; 
        }

        public bool EsMailYContrasenia(string email, string contrasenia)
        {
            return Email == email && _contrasenia == contrasenia;
        }

       

        public bool PerteneceAlMismoEquipo(Equipo equipo)
        {
            return this.MiEquipo.Id == equipo.Id;
        }

        public override string ToString()
        {
            return $"Nombre Completo: {_nombreUsuario} {_apellido} - Equipo: {MiEquipo.NombreEquipo} - Email: {Email} - Rol: {Rol}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Usuario elOtroUsuario && Email == elOtroUsuario.Email;
        }

        public int CompareTo(Usuario? other)
        {
            return Email.CompareTo(other.Email);

        }
    }
}
