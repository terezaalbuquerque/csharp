using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefas.Data;
using ProjetoTarefas.Models;

namespace ProjetoTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.Include(t => t.Projeto).ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult<Tarefa>> CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarefas), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut]

        public async Task<IActionResult> ConcluirTarefas(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            tarefa.Concluida = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            tarefa.Concluida = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}