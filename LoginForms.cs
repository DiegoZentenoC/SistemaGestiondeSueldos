using System;
using System.Windows.Forms;

namespace SistemaCalculoSueldos
{
    public partial class LoginForms : Form
    {
        public LoginForms()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validar las credenciales del administrador
            if (txtUsuario.Text == "admin" && txtContraseña.Text == "admin123")
            {
                // Crear una instancia del administrador
                Administrador admin = new Administrador(txtUsuario.Text, txtContraseña.Text);

                // Crear una nueva instancia de FormAdmin y pasar el administrador
                FormAdmin formAdmin = new FormAdmin(admin);
                formAdmin.Show(); // Muestra el formulario de administración

                // Ocultar el formulario de login
                this.Hide();
            }
            else if (txtUsuario.Text == "usuario" && txtContraseña.Text == "usuario123")
            {
                // Crear una nueva instancia de la interfaz de consulta para usuarios
                FormUsuario formUsuario = new FormUsuario();
                formUsuario.Show(); // Muestra la interfaz para usuarios

                // Ocultar el formulario de login
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.");
            }
        }


    }
}




