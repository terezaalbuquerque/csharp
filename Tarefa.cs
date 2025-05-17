using System;

namespace ProjetoTarefas.Models;
public class Tarefa
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; } //opcional
    public bool Concluida { get; set; }
    public int ProjetoId { get; set; }
    public required Projeto Projeto { get; set; } //Entity - permite navegar de uma tarefa para outra
}