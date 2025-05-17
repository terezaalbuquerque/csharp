using System.Diagnostics;

namespace ProjetoTarefas.Models;
public class Projeto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required List<Tarefa> Tarefas { get; set; }
}

