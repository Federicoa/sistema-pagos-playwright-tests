using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PagoUnico : Pago
    {
        DateTime _fechaPago;
        int _numeroRecibo;
        bool _fueEnEfectivo;

        public PagoUnico() { }
        public PagoUnico(MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario, string descripcion, decimal montoPago, DateTime fechaPago, int numeroRecibo, bool fueEnEfectivo) : base(metodoDePago, tipoDeGasto, usuario, descripcion, montoPago)
        {
            _fechaPago = fechaPago;
            _numeroRecibo = numeroRecibo;
            
            _fueEnEfectivo = fueEnEfectivo;
        }

        public DateTime FechaPago { get => _fechaPago; set => _fechaPago = value; }
        public bool FueEnEfectivo { get => _fueEnEfectivo; set => _fueEnEfectivo = value; }
        public int NumeroRecibo { get => _numeroRecibo; set => _numeroRecibo = value; }

       

        public override decimal CalcularMontoTotal()
        {
            if (MetodoDePago == MetodoDePago.EFECTIVO)
            {
                // Si fue en efectivo, se aplica un descuento del 20%
                return base.CalcularMontoTotal() * 0.8m;
            }
            else
            {
                // Caso contrario, se aplica un descuento del 10%
                return base.CalcularMontoTotal() * 0.9m;
            }
        }

        public override bool CorrespondeAlMesActual()
        {
            DateTime ahora = DateTime.Now;
            return FechaPago.Month == ahora.Month && FechaPago.Year == ahora.Year;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Fecha de Pago: {_fechaPago} - Numero de Recibo: {_numeroRecibo}";
        }

        public override bool EsDelMes(int mes, int anio)
        {
            return FechaPago.Month == mes && FechaPago.Year == anio;
        }
    }
}
