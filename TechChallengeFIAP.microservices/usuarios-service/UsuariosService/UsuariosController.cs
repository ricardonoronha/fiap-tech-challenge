using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        // Lista em memória para testes
        private static readonly List<Usuario> Usuarios = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            Usuarios.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Usuario usuarioAtualizado)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return NotFound();

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return NotFound();

            Usuarios.Remove(usuario);
            return NoContent();
        }
    }

  
    public class Usuario
    {
        public Guid Id { get; set; }

    public string NomeCompleto { get; set; } = string.Empty;

    public string NomeUsuario { get; set; } = string.Empty;

    public string EmailUsuario { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public string HashSenha { get; set; } = string.Empty;

    public bool EhAdministrador { get; set; }

    public bool EhAtivo { get; set; }
    }
}