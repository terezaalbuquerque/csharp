using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefas.Data;
using ProjetoTarefas.Models;

namespace ProjetoTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/projetos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetProjetos()
        {
            return await _context.Projetos
                                 .Include(p => p.Tarefas) // Inclui tarefas no resultado (opcional)
                                 .ToListAsync();
        }

        // POST: api/projetos
        [HttpPost]
        public async Task<ActionResult<Projeto>> CriarProjeto(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjetos), new { id = projeto.Id }, projeto);
        }

        // PUT: api/projetos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProjeto(int id, Projeto projeto)
        {
            if (id != projeto.Id)
            {
                return BadRequest("O ID da URL não bate com o ID do projeto enviado.");
            }

            _context.Entry(projeto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projetos.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/projetos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProjeto(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
                return NotFound();

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
