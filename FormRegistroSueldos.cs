using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace SistemaCalculoSueldos
{
    public partial class FormRegistroSueldos : Form
    {
        private string rutEmpleado;
        private decimal valorHora;
        private decimal valorExtra;

        public FormRegistroSueldos(string rut)
        {
            InitializeComponent();
            this.rutEmpleado = rut;
        }

        private void FormRegistroSueldos_Load(object sender, EventArgs e)
        {
            // Inicialización de elementos
            cmbAFP.Items.Add("CUPRUM");
            cmbAFP.Items.Add("MODELO");
            cmbAFP.Items.Add("CAPITAL");
            cmbAFP.Items.Add("PROVIDA");

            cmbSalud.Items.Add("FONASA");
            cmbSalud.Items.Add("CONSALUD");
            cmbSalud.Items.Add("MASVIDA");
            cmbSalud.Items.Add("BANMEDICA");

            CargarDatosEmpleado();
        }

        private void CargarDatosEmpleado()
        {
            // Crear una consulta SQL para obtener los datos del empleado basado en el RUT
            BD bd = new BD();
            string query = "SELECT Nombre_Empleado, Valor_Hora, Valor_Extra FROM Empleados WHERE RUT_Empleado = @RUT_Empleado";

            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@RUT_Empleado", this.rutEmpleado)
            };

            DataTable dtEmpleado = bd.EjecutarConsulta(query, parametros);

            if (dtEmpleado.Rows.Count > 0)
            {
                DataRow empleado = dtEmpleado.Rows[0];
                string nombre = empleado["Nombre_Empleado"].ToString();
                valorHora = Convert.ToDecimal(empleado["Valor_Hora"]);
                valorExtra = Convert.ToDecimal(empleado["Valor_Extra"]);

                lblNombreEmpleado.Text = nombre;
            }
            else
            {
                MessageBox.Show("Empleado no encontrado.");
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHorasTrabajadas.Text) || string.IsNullOrEmpty(txtHorasExtras.Text))
                {
                    MessageBox.Show("Por favor, ingresa las horas trabajadas y horas extras.");
                    return;
                }

                if (!int.TryParse(txtHorasTrabajadas.Text, out int horasTrabajadas) || !int.TryParse(txtHorasExtras.Text, out int horasExtras))
                {
                    MessageBox.Show("Por favor, ingresa valores numéricos válidos para horas trabajadas y horas extras.");
                    return;
                }

                decimal sueldoBruto = (horasTrabajadas * valorHora) + (horasExtras * valorExtra);
                txtSueldoBruto.Text = sueldoBruto.ToString("C0", CultureInfo.GetCultureInfo("es-CL")); // Formato en CLP

                // Cálculo de descuentos aquí (AFP, Salud, etc.)
                decimal descuentoAFP = CalcularDescuentoAFP(sueldoBruto, cmbAFP.SelectedItem?.ToString());
                decimal descuentoSalud = CalcularDescuentoSalud(sueldoBruto, cmbSalud.SelectedItem?.ToString());

                decimal sueldoLiquido = sueldoBruto - descuentoAFP - descuentoSalud;
                txtSueldoLiquido.Text = sueldoLiquido.ToString("C0", CultureInfo.GetCultureInfo("es-CL")); // Formato en CLP
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el cálculo: " + ex.Message);
            }
        }

        // Método para calcular el descuento AFP
        private decimal CalcularDescuentoAFP(decimal sueldoBruto, string afp)
        {
            decimal descuento = 0;
            if (string.IsNullOrEmpty(afp)) return descuento;

            switch (afp)
            {
                case "CUPRUM":
                    descuento = sueldoBruto * 0.07m;
                    break;
                case "MODELO":
                    descuento = sueldoBruto * 0.09m;
                    break;
                case "CAPITAL":
                    descuento = sueldoBruto * 0.12m;
                    break;
                case "PROVIDA":
                    descuento = sueldoBruto * 0.13m;
                    break;
                default:
                    MessageBox.Show("AFP no válida.");
                    break;
            }
            return descuento;
        }

        // Método para calcular el descuento de salud
        private decimal CalcularDescuentoSalud(decimal sueldoBruto, string salud)
        {
            decimal descuento = 0;
            if (string.IsNullOrEmpty(salud)) return descuento;

            switch (salud)
            {
                case "FONASA":
                    descuento = sueldoBruto * 0.12m;
                    break;
                case "CONSALUD":
                    descuento = sueldoBruto * 0.13m;
                    break;
                case "MASVIDA":
                    descuento = sueldoBruto * 0.14m;
                    break;
                case "BANMEDICA":
                    descuento = sueldoBruto * 0.15m;
                    break;
                default:
                    MessageBox.Show("Plan de salud no válido.");
                    break;
            }
            return descuento;
        }

        // Evento para limpiar los campos del formulario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtHorasTrabajadas.Clear();
            txtHorasExtras.Clear();
            txtSueldoBruto.Clear();
            txtSueldoLiquido.Clear();
            cmbAFP.SelectedIndex = -1;
            cmbSalud.SelectedIndex = -1;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            FormUsuario formUsuario = new FormUsuario();
            formUsuario.Show();
            this.Hide();
        }

        // Evento para el botón Listar
        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                BD bd = new BD();
                string query = "SELECT RUT_Empleado, Nombre_Empleado, Direccion_Empleado, Telefono_Empleado, Valor_Hora, Valor_Extra, Sueldo_Bruto, Sueldo_Liquido FROM Empleados";
                DataTable dt = bd.EjecutarConsulta(query);

                if (dt.Rows.Count > 0) 
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No se encontraron datos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar los empleados: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHorasTrabajadas.Text) || string.IsNullOrEmpty(txtHorasExtras.Text))
                {
                    MessageBox.Show("Por favor, ingresa las horas trabajadas y horas extras.");
                    return;
                }

                if (!int.TryParse(txtHorasTrabajadas.Text, out int horasTrabajadas) || !int.TryParse(txtHorasExtras.Text, out int horasExtras))
                {
                    MessageBox.Show("Por favor, ingresa valores numéricos válidos para horas trabajadas y horas extras.");
                    return;
                }

                // Validar selección de AFP y Salud
                if (cmbAFP.SelectedItem == null || cmbSalud.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecciona un plan de AFP y Salud.");
                    return;
                }

                // Calcular el Sueldo Bruto y el Sueldo Líquido
                decimal sueldoBruto = (horasTrabajadas * valorHora) + (horasExtras * valorExtra);
                decimal descuentoAFP = CalcularDescuentoAFP(sueldoBruto, cmbAFP.SelectedItem.ToString());
                decimal descuentoSalud = CalcularDescuentoSalud(sueldoBruto, cmbSalud.SelectedItem.ToString());
                decimal sueldoLiquido = sueldoBruto - descuentoAFP - descuentoSalud;

                // Llamar al método para guardar en la base de datos
                ActualizarSueldoEnBaseDatos(sueldoBruto, sueldoLiquido);

                MessageBox.Show("Sueldo actualizado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el sueldo: " + ex.Message);
            }
        }

        // Método para actualizar los sueldos en la base de datos
        private void ActualizarSueldoEnBaseDatos(decimal sueldoBruto, decimal sueldoLiquido)
        {
            try
            {
                //cadena de conexión a la base de datos
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Sistemasueldos;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Empleados SET Sueldo_Bruto = @Sueldo_Bruto, Sueldo_Liquido = @Sueldo_Liquido WHERE RUT_Empleado = @RUT_Empleado";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Sueldo_Bruto", sueldoBruto);
                        cmd.Parameters.AddWithValue("@Sueldo_Liquido", sueldoLiquido);
                        cmd.Parameters.AddWithValue("@RUT_Empleado", rutEmpleado); 

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la base de datos: " + ex.Message);
            }
        }
    }
}


































