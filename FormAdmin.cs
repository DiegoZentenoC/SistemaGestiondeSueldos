using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace SistemaCalculoSueldos
{
    public partial class FormAdmin : Form
    {
        private List<Empleado> listaEmpleados = new List<Empleado>();
        private Administrador administrador;

        public FormAdmin(Administrador admin)
        {
            InitializeComponent();
            this.administrador = admin;  // Inicializa el administrador
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verifica que todos los campos estén llenos
            if (string.IsNullOrEmpty(txtRut.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtTelefono.Text) ||
                string.IsNullOrEmpty(txtValorHora.Text) || string.IsNullOrEmpty(txtValorExtra.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            try
            {
                // Crear una nueva instancia de Empleado
                Empleado nuevoEmpleado = new Empleado(
                    txtRut.Text,
                    txtNombre.Text,
                    txtDireccion.Text,
                    txtTelefono.Text,
                    Convert.ToDecimal(txtValorHora.Text),
                    Convert.ToDecimal(txtValorExtra.Text)
                );

                // Verificar si el empleado ya existe
                if (EmpleadoExiste(nuevoEmpleado.Rut))
                {
                    ActualizarEmpleadoEnBaseDatos(nuevoEmpleado);
                    MessageBox.Show("Datos del empleado actualizados correctamente.");
                }
                else
                {
                    GuardarEmpleadoEnBaseDatos(nuevoEmpleado);
                    MessageBox.Show("Empleado guardado correctamente.");
                }

                // Limpiar campos después de guardar o actualizar
                LimpiarCampos();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error en el formato de los datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar empleado: " + ex.Message);
            }
        }

        private bool EmpleadoExiste(string rutEmpleado)
        {
            bool existe = false;
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaSueldosDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Empleados WHERE RUT_Empleado = @RUT_Empleado";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RUT_Empleado", rutEmpleado);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
            }

            return existe;
        }

        private void GuardarEmpleadoEnBaseDatos(Empleado empleado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaSueldosDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Empleados (RUT_Empleado, Nombre_Empleado, Direccion_Empleado, Telefono_Empleado, Valor_Hora, Valor_Extra) " +
                               "VALUES (@RUT_Empleado, @Nombre_Empleado, @Direccion_Empleado, @Telefono_Empleado, @Valor_Hora, @Valor_Extra)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RUT_Empleado", empleado.Rut);
                    cmd.Parameters.AddWithValue("@Nombre_Empleado", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion_Empleado", empleado.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono_Empleado", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@Valor_Hora", empleado.ValorHora);
                    cmd.Parameters.AddWithValue("@Valor_Extra", empleado.ValorExtra);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ActualizarEmpleadoEnBaseDatos(Empleado empleado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaSueldosDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Empleados SET Nombre_Empleado = @Nombre_Empleado, Direccion_Empleado = @Direccion_Empleado, " +
                               "Telefono_Empleado = @Telefono_Empleado, Valor_Hora = @Valor_Hora, Valor_Extra = @Valor_Extra " +
                               "WHERE RUT_Empleado = @RUT_Empleado";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RUT_Empleado", empleado.Rut);
                    cmd.Parameters.AddWithValue("@Nombre_Empleado", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion_Empleado", empleado.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono_Empleado", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@Valor_Hora", empleado.ValorHora);
                    cmd.Parameters.AddWithValue("@Valor_Extra", empleado.ValorExtra);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRut.Text))
                {
                    MessageBox.Show("Por favor, ingresa el RUT del empleado a eliminar.");
                    return;
                }

                string rutEmpleado = txtRut.Text;

                string connectionString = ConfigurationManager.ConnectionStrings["SistemaSueldosDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Empleados WHERE RUT_Empleado = @RUT_Empleado";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RUT_Empleado", rutEmpleado);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Empleado eliminado correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar empleado: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtRut.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtValorHora.Clear();
            txtValorExtra.Clear();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            dataGridViewEmpleados.Rows.Clear();
            dataGridViewEmpleados.Columns.Clear();

            // Agregar columnas al DataGridView
            dataGridViewEmpleados.Columns.Add("RUT_Empleado", "RUT del Empleado");
            dataGridViewEmpleados.Columns.Add("Nombre_Empleado", "Nombre del Empleado");
            dataGridViewEmpleados.Columns.Add("Direccion_Empleado", "Dirección");
            dataGridViewEmpleados.Columns.Add("Telefono_Empleado", "Teléfono");
            dataGridViewEmpleados.Columns.Add("Valor_Hora", "Valor Hora");
            dataGridViewEmpleados.Columns.Add("Valor_Extra", "Valor Extra");
            dataGridViewEmpleados.Columns.Add("Sueldo_Bruto", "Sueldo Bruto");
            dataGridViewEmpleados.Columns.Add("Sueldo_Liquido", "Sueldo Líquido");

            BD bd = new BD();
            string query = "SELECT RUT_Empleado, Nombre_Empleado, Direccion_Empleado, Telefono_Empleado, Valor_Hora, Valor_Extra, Sueldo_Bruto, Sueldo_Liquido FROM Empleados";

            DataTable dtEmpleados = bd.EjecutarConsulta(query);

            // Agregar filas a la tabla del DataGridView
            foreach (DataRow row in dtEmpleados.Rows)
            {
                dataGridViewEmpleados.Rows.Add(
                    row["RUT_Empleado"],
                    row["Nombre_Empleado"],
                    row["Direccion_Empleado"],
                    row["Telefono_Empleado"],
                    row["Valor_Hora"],
                    row["Valor_Extra"],
                    row["Sueldo_Bruto"],
                    row["Sueldo_Liquido"]
                );
            }
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRut.Text))
            {
                MessageBox.Show("Por favor, ingresa el RUT del empleado a modificar.");
                return;
            }

            try
            {
                Empleado empleadoModificado = new Empleado(
                    txtRut.Text,
                    txtNombre.Text,
                    txtDireccion.Text,
                    txtTelefono.Text,
                    Convert.ToDecimal(txtValorHora.Text),
                    Convert.ToDecimal(txtValorExtra.Text)
                );

                ActualizarEmpleadoEnBaseDatos(empleadoModificado);
                MessageBox.Show("Empleado actualizado correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el empleado: " + ex.Message);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForms loginForm = new LoginForms();
            loginForm.Show();
        }

        private void btnAbrirRegistroSueldos_Click(object sender, EventArgs e)
        {
            FormRegistroSueldos formRegistroSueldos = new FormRegistroSueldos("RUT_DE_EJEMPLO");
            formRegistroSueldos.ShowDialog();
        }

        private void dataGridViewEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cargar los datos del empleado seleccionado en los campos de texto
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEmpleados.Rows[e.RowIndex];

                // Asegura de que los nombres de las columnas coincidan con los nombres en tu DataGridView o base de datos
                txtRut.Text = row.Cells["RUT_Empleado"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre_Empleado"].Value.ToString();
                txtDireccion.Text = row.Cells["Direccion_Empleado"].Value.ToString();
                txtTelefono.Text = row.Cells["Telefono_Empleado"].Value.ToString();
                txtValorHora.Text = row.Cells["Valor_Hora"].Value.ToString();
                txtValorExtra.Text = row.Cells["Valor_Extra"].Value.ToString();
            }
        }


        // Método para limpiar los campos al hacer clic en el botón Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
































