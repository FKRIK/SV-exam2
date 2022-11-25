using System.Collections.Generic;
using System.Linq;
using api.Models;
using API_Copa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class JogoController : ControllerBase
    {
        private readonly Context _context;
        public JogoController(Context context) =>
            _context = context;

        // ----------- ORIGINAL -------------------------------------
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Jogo jogo)
        {
            jogo.SelecaoA = _context.Selecoes.Find(jogo.SelecaoA.Id);
            jogo.SelecaoB = _context.Selecoes.Find(jogo.SelecaoB.Id);

            _context.Jogos.Add(jogo);
            _context.SaveChanges();
            return Created("", jogo);
            
        }


        // [HttpPost]
        // [Route("cadastrar")]
        // public IActionResult Cadastrar(ObjectResult id)
        // {
        //     var result = _context.Selecoes.FirstOrDefault(selecaoCadastrada => selecaoCadastrada.Id.Equals(id));

        //     if(result == null){
        //         return BadRequest();
        //     } 
        //     else{

        //     }


        //     _context.Jogos.Add(jogo);
        //     _context.SaveChanges();
        //     return Created("", jogo);

        //     // if (jogo.SelecaoA == _context.Selecoes.Find(jogo.SelecaoA.Id) && jogo.SelecaoB == _context.Selecoes.Find(jogo.SelecaoB.Id))
        //     // {
        //     //     _context.Jogos.Add(jogo);
        //     //     _context.SaveChanges();
        //     //     return Created("", jogo);
        //     // }
        //     // return BadRequest();
            
        // }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            List<Jogo> jogos = _context.Jogos.Include(x => x.SelecaoA).Include(x => x.SelecaoB).ToList();
            return jogos.Count != 0 ? Ok(jogos) : NotFound();
        }
    }
}