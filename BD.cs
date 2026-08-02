using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaCalculoSueldos
{
    public class BD
    {
        private SqlConnection conexion;

        // Constructor: Inicializa la conexión usando la cadena de conexión del App.config
        public BD()
        {
            // Obtiene la cadena de conexión desde App.config
            string connectionString = ConfigurationManager.ConnectionStrings["SistemaSueldosDB"].ConnectionString;
            conexion = new SqlConnection(connectionString);
        }

        // Método para abrir la conexión a la base de datos
        public void AbrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo abrir la conexión con la base de datos: " + ex.Message);
            }
        }

        // Método para cerrar la conexión a la base de datos
        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cerrar la conexión con la base de datos: " + ex.Message);
            }
        }

        // Método para ejecutar un comando SQL que no devuelve resultados (INSERT, UPDATE, DELETE)
        public void EjecutarComando(string query, SqlParameter[] parametros = null)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar comando SQL: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        // Método para ejecutar una consulta SQL que devuelve resultados (SELECT)
        public DataTable EjecutarConsulta(string query, SqlParameter[] parametros = null)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar consulta SQL: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }
    }
}


