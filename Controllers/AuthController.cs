using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefas.Data;
using ProjetoTarefas.Models;

namespace ProjetoTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjetoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get() =>
            Ok(await _context.Projetos.ToListAsync());

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = projeto.Id }, projeto);
        }
    }
}
