using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCalculoSueldos
{
    public class UsuarioBase
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        // Constructor que inicializa el usuario y la contraseña
        public UsuarioBase(string nombreUsuario, string contraseña)
        {
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña;
        }

        // Método para validar el login
        public bool ValidarLogin(string usuario, string contraseña)
        {
            return NombreUsuario == usuario && Contraseña == contraseña;
        }
    }
}
