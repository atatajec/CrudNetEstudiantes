using CrudEstudiante.Models;

namespace CrudEstudiante.Data.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> ObtenerEstudiantes();
        Task<Estudiante> ObtenerPorId(int id);
        Task Agregar(Estudiante estudiante);
        Task Editar(Estudiante estudiante);
        Task Eliminar(int id);

    }
}
