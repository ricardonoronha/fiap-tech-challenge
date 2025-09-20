using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private static readonly List<Jogo> Jogos = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Jogos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var jogo = Jogos.FirstOrDefault(j => j.Id == id);
            if (jogo == null) return NotFound();
            return Ok(jogo);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Jogo jogo)
        {
            jogo.Id = Guid.NewGuid();
            Jogos.Add(jogo);
            return CreatedAtAction(nameof(GetById), new { id = jogo.Id }, jogo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Jogo jogoAtualizado)
        {
            var jogo = Jogos.FirstOrDefault(j => j.Id == id);
            if (jogo == null) return NotFound();

            jogo.Nome = jogoAtualizado.Nome;
            jogo.Genero = jogoAtualizado.Genero;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var jogo = Jogos.FirstOrDefault(j => j.Id == id);
            if (jogo == null) return NotFound();

            Jogos.Remove(jogo);
            return NoContent();
        }
    }

    public class Jogo
{
    public Guid Id { get; set; }

    public string NomeJogo { get; set; } = string.Empty;

    public string DescricaoJogo { get; set; } = string.Empty;

    public string ClassificacaoJogo { get; set; } = "L";

    public DateTime DataLancamento { get; set; }

    public decimal ValorBase { get; set; }

    public bool EhInativo { get; set; }
}
}