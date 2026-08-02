using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCalculoSueldos
{
    public class Administrador : UsuarioBase
    {
        // Constructor que hereda de UsuarioBase
        public Administrador(string nombreUsuario, string contraseña)
            : base(nombreUsuario, contraseña)
        {
        }

        // Método para agregar un empleado a la lista de empleados
        public void AgregarEmpleado(List<Empleado> listaEmpleados, Empleado nuevoEmpleado)
        {
            listaEmpleados.Add(nuevoEmpleado);
            Console.WriteLine("Empleado agregado correctamente.");
        }

        // Método para eliminar un empleado por su RUT
        public void EliminarEmpleado(List<Empleado> listaEmpleados, string rutEmpleado)
        {
            var empleado = listaEmpleados.FirstOrDefault(e => e.Rut == rutEmpleado);
            if (empleado != null)
            {
                listaEmpleados.Remove(empleado);
                Console.WriteLine("Empleado eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Empleado no encontrado.");
            }
        }
    }
}


