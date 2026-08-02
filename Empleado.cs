using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCalculoSueldos
{
    public class Empleado
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal ValorHora { get; set; }
        public decimal ValorExtra { get; set; }
        public int HorasTrabajadas { get; set; }
        public int HorasExtras { get; set; }


        public Empleado(string rut, string nombre, string direccion, string telefono, decimal valorHora, decimal valorExtra)
        {
            Rut = rut;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            ValorHora = valorHora;
            ValorExtra = valorExtra;
        }

        // Método para calcular el sueldo bruto
        public decimal CalcularSueldoBruto()
        {
            return (HorasTrabajadas * ValorHora) + (HorasExtras * ValorExtra);
        }

        // Método para calcular el descuento AFP
        public decimal CalcularDescuentoAFP(decimal sueldoBruto, string afp)
        {
            decimal descuento = 0;
            switch (afp)
            {
                case "CUPRUM": descuento = sueldoBruto * 0.07m; break;
                case "MODELO": descuento = sueldoBruto * 0.09m; break;
                case "CAPITAL": descuento = sueldoBruto * 0.12m; break;
                case "PROVIDA": descuento = sueldoBruto * 0.13m; break;
            }
            return descuento;
        }

        // Método para calcular el descuento de salud
        public decimal CalcularDescuentoSalud(decimal sueldoBruto, string salud)
        {
            decimal descuento = 0;
            switch (salud)
            {
                case "FONASA": descuento = sueldoBruto * 0.12m; break;
                case "CONSALUD": descuento = sueldoBruto * 0.13m; break;
                case "MASVIDA": descuento = sueldoBruto * 0.14m; break;
                case "BANMEDICA": descuento = sueldoBruto * 0.15m; break;
            }
            return descuento;
        }

        // Método para calcular el sueldo líquido
        public decimal CalcularSueldoLiquido(decimal sueldoBruto, decimal descuentoAFP, decimal descuentoSalud)
        {
            return sueldoBruto - descuentoAFP - descuentoSalud;
        }
    }
}

