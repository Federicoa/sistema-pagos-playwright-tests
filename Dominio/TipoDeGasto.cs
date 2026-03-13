using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoDeGasto
    {
        string _nombre;
        string _descripcion;

        public int Id { get; set; }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        public TipoDeGasto()
        {
        }
        public TipoDeGasto(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        private void ValidarCamposVacios()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("Nombre no puede ser vacio");
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("Apellido no puede ser vacio");
            
        }

        public override bool Equals(object? obj)
        {
            return obj is TipoDeGasto elOtroTipoDeGasto && Nombre == elOtroTipoDeGasto.Nombre;
        }

        public bool EsNombreDeTipoDeGasto(string nombre)
        {
            return Nombre == nombre;
        }

        public void Validar()
        {
            ValidarCamposVacios();
        }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
