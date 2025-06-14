using CrudEstudiante.Data.Interfaces;
using CrudEstudiante.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudEstudiante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository _repo;

        public EstudianteController(IEstudianteRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _repo.ObtenerEstudiantes();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var estudiante = await _repo.ObtenerPorId(id);
            if (estudiante == null) return NotFound();
            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Estudiante estudiante)
        {
            await _repo.Agregar(estudiante);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]  Estudiante estudiante)
        {
            estudiante.Id = id;
            await _repo.Editar(estudiante);
            return Ok();    
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.Eliminar(id);
            return Ok();
        }
    }
}
