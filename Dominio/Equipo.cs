using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Equipo
    {
        public int Id { get; set; }

        public string NombreEquipo { get; set; }

        public Equipo()
        {
        }

        public Equipo(string nombreEquipo)
        {
            NombreEquipo = nombreEquipo;
        }

        public bool EsNombre(string nombreEquipo)
        {
            return NombreEquipo == nombreEquipo;
        }
    }
}