using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PagoRecurrente : Pago
    {
        DateTime _fechaInicio;
        DateTime? _fechaFin;
       

        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime? FechaFin { get => _fechaFin; set => _fechaFin = value; }

        public bool EsConLimite => FechaFin.HasValue;

        public PagoRecurrente() { }
        public PagoRecurrente(MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario, string descripcion, decimal montoPago, DateTime fechaInicio, DateTime? fechaFin) : base(metodoDePago, tipoDeGasto, usuario, descripcion, montoPago)
        {
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            
        }

        public override void Validar()
        {

            base.Validar();

           

            if (EsConLimite && FechaFin < FechaInicio)
                throw new Exception("La fecha de fin no puede ser anterior a la fecha de inicio.");
        }

      
        public int CantidadDeCuotasTotales()
        {
            if (!EsConLimite)
                throw new Exception(
                    "Un pago sin límite no tiene cantidad total de cuotas."
                );
            DateTime fechaFin = FechaFin!.Value;
            int meses = ((fechaFin.Year - _fechaInicio.Year) * 12)
            + (fechaFin.Month - _fechaInicio.Month) + 1;
            return meses;
        }

        



        public decimal CalcularImpuestoAdicional()
        {
            decimal impuesto = 0;
            if (EsConLimite && CantidadDeCuotasTotales() > 10) impuesto = 1.10m;
            else if (EsConLimite && (CantidadDeCuotasTotales() >= 6 && CantidadDeCuotasTotales() <= 9)) impuesto = 1.05m;
            else if (EsConLimite && CantidadDeCuotasTotales() <= 5) impuesto = 1.03m;
            else if (!EsConLimite) impuesto = 1.03m;
            return impuesto;

        }

        public override decimal CalcularMontoTotal()
        {
            if (EsConLimite)
            {
                int meses = CantidadDeCuotasTotales();
                decimal montoBase = meses * base.CalcularMontoTotal();
                decimal impuesto = CalcularImpuestoAdicional();
                return montoBase * impuesto;
            }
            else
            {
                decimal impuesto = CalcularImpuestoAdicional();
                return base.CalcularMontoTotal() * impuesto;
            }
        }


       
        public decimal CalcularMontoMensual()
        {
            if (EsConLimite)
            { 
                int meses = CantidadDeCuotasTotales();
                if (meses == 0) return 0;

                decimal totalConImpuesto = CalcularMontoTotal();
                //Devuelve una cuota
                return totalConImpuesto / meses;
            }
            
        
            else
            {
                // Si no tiene límite, el monto base ya representa la cuota mensual
                decimal impuesto = CalcularImpuestoAdicional();
                return base.CalcularMontoTotal() * impuesto;
            }
        }

        public override bool CorrespondeAlMesActual()
        {
            DateTime hoy = DateTime.Now;
            int mesActual = hoy.Month;
            int anioActual = hoy.Year;

            DateTime inicioMes = new DateTime(anioActual, mesActual, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            return FechaInicio <= finMes && (!EsConLimite || FechaFin!.Value >= inicioMes);
        }

        public override string ToString()
        {
            return base.ToString() + $" - Fecha de Inicio: {_fechaInicio} - Fecha de Fin: {_fechaFin}";
        }
        // Para recurrentes redefine el monto ordenado como el monto mensual


        public override decimal MontoOrdenado()
        {
            return CalcularMontoMensual();
        }

        public override bool EsDelMes(int mes, int anio)
        {
            DateTime inicioMes = new DateTime(anio, mes, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            bool empiezaAntesDeFin = FechaInicio <= finMes;
            bool terminaDespuesDeInicio = !EsConLimite || FechaFin!.Value >= inicioMes;

            return empiezaAntesDeFin && terminaDespuesDeInicio;
        }

        public override decimal CalcularMontoDelMes()
        {
            return CalcularMontoMensual();
        }

    }
}
