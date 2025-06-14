using Microsoft.Data.SqlClient;
using CrudEstudiante.Data.Interfaces;
using CrudEstudiante.Models;
using System.Data.SqlTypes;

namespace CrudEstudiante.Data
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly string _conexion;

        public EstudianteRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("ConexionSql");
        }

        private async Task conectarSP(string nomSP, Estudiante estudiante)
        {
            using (var conn = new SqlConnection(_conexion))
            {
                var cmd = new SqlCommand(nomSP, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                if (estudiante != null)
                {
                    if(estudiante.Nombre != null)
                    {
                        cmd.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    }

                    if (estudiante.Edad != null)
                    {
                        cmd.Parameters.AddWithValue("@Edad", estudiante.Edad);
                    }

                    if (estudiante.Carrera != null)
                    {
                        cmd.Parameters.AddWithValue("@Carrera", estudiante.Carrera);
                    }
                }

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Agregar(Estudiante estudiante)
        {
            await conectarSP("SP_AgregarEstudiante", estudiante);
        }

        public async Task Editar(Estudiante estudiante)
        {
            await conectarSP("SP_ActualizarEstudiante", estudiante);
        }

        public async Task Eliminar(int id)
        {
            using (var conn = new SqlConnection(_conexion))
            {
                var cmd = new SqlCommand("SP_EliminarEstudiante", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Estudiante>> ObtenerEstudiantes()
        {
            var lista = new List<Estudiante>();

            using (var conn = new SqlConnection(_conexion))
            {
                var cmd = new SqlCommand("SP_ObtenerEstudiantes", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                await conn.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    lista.Add(new Estudiante
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Edad = Convert.ToInt32(reader["Edad"]),
                        Carrera = reader["Carrera"].ToString(),
                    });
                }
            }

            return lista;
        }

        public async Task<Estudiante> ObtenerPorId(int id)
        {
            Estudiante estudiante = null;

            using (var conn = new SqlConnection(_conexion))
            {
                var cmd = new SqlCommand("SP_ObtenerEstudiantePorId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                await conn.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    estudiante = new Estudiante
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Edad = Convert.ToInt32(reader["Edad"]),
                        Carrera = reader["Carrera"].ToString(),
                    };
                }

            }

            return estudiante;
        }
    }
}
