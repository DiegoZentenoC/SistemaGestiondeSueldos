using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaCalculoSueldos
{
    public partial class FormUsuario : Form
    {
        public FormUsuario()
        {
            InitializeComponent();  
        }

        // Evento del botón Consultar para consultar los datos del usuario y su sueldo
        private void btnConsultarSueldo_Click(object sender, EventArgs e)
        {
            // Verificar que el campo RUT no esté vacío
            if (string.IsNullOrEmpty(txtRutUsuario.Text))
            {
                MessageBox.Show("Por favor, ingresa tu RUT.");
                return;
            }

            try
            {
                // Obtener el RUT del TextBox
                string rutUsuario = txtRutUsuario.Text;

                // Crear una consulta SQL para obtener los datos del empleado
                BD bd = new BD();
                string query = "SELECT Nombre_Empleado, Direccion_Empleado, Telefono_Empleado, Valor_Hora, Valor_Extra " +
                               "FROM Empleados WHERE RUT_Empleado = @RUT_Empleado";

                SqlParameter[] parametros = new SqlParameter[] {
                    new SqlParameter("@RUT_Empleado", rutUsuario)
                };

                // Ejecutar la consulta y obtener los resultados
                DataTable dtEmpleado = bd.EjecutarConsulta(query, parametros);

                // Verificar si se encontró el empleado
                if (dtEmpleado.Rows.Count > 0)
                {
                    DataRow empleado = dtEmpleado.Rows[0];
                    string nombre = empleado["Nombre_Empleado"].ToString();
                    string direccion = empleado["Direccion_Empleado"].ToString();
                    string telefono = empleado["Telefono_Empleado"].ToString();
                    decimal valorHora = Convert.ToDecimal(empleado["Valor_Hora"]);
                    decimal valorExtra = Convert.ToDecimal(empleado["Valor_Extra"]);
                    decimal sueldoBruto = (valorHora * 160) + valorExtra; // Calculo Sueldo bruto

                    

                    // Abrir directamente el formulario de registro de sueldos con el RUT del empleado
                    FormRegistroSueldos formRegistroSueldos = new FormRegistroSueldos(rutUsuario);
                    formRegistroSueldos.Show(); // Mostrar la ventana de registro de sueldos

                    // Opción para ocultar el formulario actual si es necesario
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Empleado no encontrado. Verifica el RUT ingresado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar los datos: " + ex.Message);
            }
        }

        // Evento del botón Volver para regresar al formulario de login
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Mostrar el formulario de login o el formulario principal
            LoginForms loginForm = new LoginForms();
            loginForm.Show();
        }
    }
}









