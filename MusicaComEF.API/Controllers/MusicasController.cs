using Microsoft.AspNetCore.Mvc;
using MusicaComEF.API.Data;
using MusicaComEF.API.DTOs;
using MusicaComEF.API.Models;
using MusicaComEF.API.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicaComEF.API.Controllers
{
    
    [ApiController]
    [Route("api/musicas")]
    public class MusicasController : ControllerBase
    {

        private readonly MusicasDbContext _dbContext;


        public MusicasController(MusicasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<MusicaModel>> Get([FromQuery] string? PorNomeDoAlbum, [FromQuery] string? PorNomeDoArtista, [FromQuery] string? PorNomeDaMusica)
        {

            if (!string.IsNullOrEmpty(PorNomeDoAlbum))
            {
                return _dbContext.Musicas.Where(musicaDb => musicaDb.Album.Nome.Contains(PorNomeDoAlbum)).ToList();
            }
            else if (!string.IsNullOrEmpty(PorNomeDoArtista))
            {
                return _dbContext.Musicas.Where(musicaDb=> musicaDb.Artista.Nome.Contains(PorNomeDoArtista)).ToList();
            }

            else if (!string.IsNullOrEmpty(PorNomeDaMusica))
            {

                return _dbContext.Musicas.Where(musicaDb => musicaDb.Nome.Contains(PorNomeDaMusica)).ToList();

            }

            return Ok( _dbContext.Musicas.ToList());

        }



        [HttpGet("{id}")]
        public ActionResult<MusicaModel> GetPorId([FromRoute] int id)
        {

            var musica = _dbContext.Musicas.Find(id);

            if (musica == null) return NotFound(new RetornoComFalhaViewModel("Musica Não Encontrada"));

            return Ok(musica);
        }

      
        [HttpPost]
        public ActionResult<MusicaModel> Post([FromBody] MusicaDTO musicaDTO)
        {
            if (_dbContext.Musicas.Any(MusicaDb => MusicaDb.Nome == musicaDTO.Nome))
            {
                return BadRequest(new RetornoComFalhaViewModel("Já Contém Esta Musica No Banco"));
            }
            var musica = new MusicaModel
            {
                Nome = musicaDTO.Nome,
                Duracao = musicaDTO.Duracao,
                AlbumId = musicaDTO.AlbumId,
                ArtistaId = musicaDTO.ArtistaId,
                PlayListId = musicaDTO.PlayListId
            };

            _dbContext.Musicas.Add(musica);

            _dbContext.SaveChanges();

            return Created("api/musicas", musica);
        }

        // PUT api/<MusicaController>/5
        [HttpPut("{id}")]
        public ActionResult<MusicaModel> Put([FromRoute] int id, [FromBody] MusicaDTO musicaDTO)
        {

            var musica = _dbContext.Musicas.Find(id);

            if (musica == null) return NotFound(new RetornoComFalhaViewModel("Musica Não Encontrada"));

            if (_dbContext.Musicas.Any(MusicaDb => MusicaDb.Nome == musicaDTO.Nome &&  MusicaDb.Id != id))
            {
                return BadRequest(new RetornoComFalhaViewModel("Já Contém Esta Musica No Banco"));
            }
            musica.Nome = musicaDTO.Nome;
            musica.Duracao = musicaDTO.Duracao;
            musica.AlbumId = musicaDTO.AlbumId;
            musica.ArtistaId = musicaDTO.ArtistaId;
            musica.PlayListId = musicaDTO.PlayListId;

            _dbContext.SaveChanges();

            return Ok(musica);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {

            var musica = _dbContext.Musicas.Find(id);

            if (musica == null) return NotFound(new RetornoComFalhaViewModel("Musica Não Encontrada"));

            _dbContext.Musicas.Remove(musica);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
