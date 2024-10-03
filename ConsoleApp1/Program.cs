using System;
using System.Data;
using System.Data.SqlClient;

namespace AsignarSaldos
{
    class Program
    {
      
        static void Main(string[] args)
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=.;Database=DBTest;Integrated Security=true;";

            try
            {
                // Llamar al método que ejecuta el procedimiento almacenado
                EjecutarAsignarSaldos(connectionString);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error de base de datos: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            // Espera a que el usuario presione una tecla antes de cerrar la aplicación
            Console.ReadLine();
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado 'AsignarMontos' y muestra los saldos asignados a los gestores.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos.</param>
        static void EjecutarAsignarSaldos(string connectionString)
        {
            // Establece la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abre la conexión
                connection.Open();

                // Crear un comando para llamar al procedimiento almacenado
                using (SqlCommand command = new SqlCommand("AsignarMontos", connection))
                {
                    // Especifica que el comando es un procedimiento almacenado
                    command.CommandType = CommandType.StoredProcedure;

                    // Ejecuta el comando y obtiene un lector de datos
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Muestra los encabezados de la salida
                        Console.WriteLine("Saldos Asignados a Gestores:");
                        Console.WriteLine("ID | Gestor ID | Saldo Monto");

                        // Lee los resultados devueltos por el procedimiento almacenado
                        while (reader.Read())
                        {
                            // Obtiene el ID del registro, el ID del gestor y el monto del saldo
                            int id = reader.GetInt32(0);
                            int gestorId = reader.GetInt32(1);
                            decimal saldoMonto = reader.GetDecimal(2);

                            // Muestra los datos en la consola
                            Console.WriteLine($"{id} | {gestorId} | {saldoMonto}");
                        }
                    }
                }
            }
        }
    }
}
