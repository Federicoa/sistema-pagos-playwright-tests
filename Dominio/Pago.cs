using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum MetodoDePago { CREDITO, DEBITO, EFECTIVO}

    public abstract class Pago : IComparable<Pago>, IValidable
    {
        
        int _id;
        MetodoDePago _metodoDePago;
        
        string _descripcion;
        decimal _montoPago;

        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public decimal MontoPago { get => _montoPago; set => _montoPago = value; }
        public MetodoDePago MetodoDePago { get => _metodoDePago; set => _metodoDePago = value; }
        public TipoDeGasto TipoDeGasto { get; set; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        protected Pago()
        {
        }

        public Pago(MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario, string descripcion, decimal montoPago)
        {
            
            _metodoDePago = metodoDePago;
            TipoDeGasto = tipoDeGasto;
            Usuario = usuario;
            _descripcion = descripcion;
            _montoPago = montoPago;
        }

        // Para pagos únicos utiliza el monto de pago, para recurrentes lo redefine en la clase hija
        public virtual decimal CalcularMontoTotal()
        {
            return _montoPago;
        }

        public bool ExistePagoConEseTipoDeGasto(TipoDeGasto tipoDeGasto)
        {
            return TipoDeGasto == tipoDeGasto;
        }

        public virtual decimal CalcularMontoDelMes()
        {
            return CalcularMontoTotal();
        }

        public bool EsDeUsuario(Usuario? usuario)
        {
            return Usuario == usuario;
        }

        public override string ToString()
        {
            return $"ID: {Id} - Metodo de Pago: {_metodoDePago} - Tipo de Gasto: {TipoDeGasto} - Usuario: {Usuario} - Descripcion: {_descripcion} - Monto Total: {CalcularMontoTotal()}";
        }

        public int CompareTo(Pago other)
        {
            return -this.MontoOrdenado().CompareTo(other.MontoOrdenado());
        }



        public abstract bool CorrespondeAlMesActual();

        // Por defecto devuelve el monto total
        public virtual decimal MontoOrdenado()
        {
            return CalcularMontoTotal();
        }

        public abstract bool EsDelMes(int mes, int anio);


        public virtual void Validar()
        {
            if(string.IsNullOrEmpty(Descripcion)) throw new Exception("La descripcion no puede ser vacía");
            if (MontoPago <= 0)
                throw new Exception("El monto debe ser mayor a cero.");
        }
    }
}
