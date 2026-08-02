using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCalculoSueldos
{
    public class Usuario : UsuarioBase
    {
        public Usuario(string nombreUsuario, string contraseña)
            : base(nombreUsuario, contraseña)
        {
        }

        // Método para consultar y calcular el sueldo líquido de un empleado
        public decimal ConsultarSueldoLiquido(List<Empleado> listaEmpleados, string rutEmpleado)
        {
            var empleado = listaEmpleados.FirstOrDefault(e => e.Rut == rutEmpleado);
            if (empleado != null)
            {
                decimal sueldoBruto = empleado.CalcularSueldoBruto();
                decimal descuentoAFP = empleado.CalcularDescuentoAFP(sueldoBruto, "CUPRUM");  
                decimal descuentoSalud = empleado.CalcularDescuentoSalud(sueldoBruto, "FONASA");  
                return empleado.CalcularSueldoLiquido(sueldoBruto, descuentoAFP, descuentoSalud);
            }
            else
            {
                throw new Exception("Empleado no encontrado.");
            }
        }
    }
}
