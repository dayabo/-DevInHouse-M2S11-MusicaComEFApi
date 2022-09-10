using Microsoft.AspNetCore.Mvc;
using MusicaComEF.API.Data;
using MusicaComEF.API.DTOs;
using MusicaComEF.API.Models;
using MusicaComEF.API.ViewModels;

namespace MusicaComEF.API.Controllers
{
    [ApiController]
    [Route("api/artistas")]
    public class ArtistasController : ControllerBase
    {
        private readonly MusicasDbContext _dbContext;


        public ArtistasController(MusicasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<ArtistaModel>> Get()
        {
            return _dbContext.Artistas.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ArtistaModel> GetPorId([FromRoute] int id)
        { 

            var artista = _dbContext.Artistas.Find(id);

            if (artista == null) return NotFound(new RetornoComFalhaViewModel("Artista Não Encontrado"));

            return Ok(artista);
        }

        [HttpPost]
        public ActionResult<ArtistaModel> Post([FromBody] ArtistaDTO artistaDTO)
        {
            if(_dbContext.Artistas.Any(artistaDb => artistaDb.Nome == artistaDTO.Nome))
            {
                return BadRequest(new RetornoComFalhaViewModel("Já Contém Este Artista No Banco"));
            }

            var artista = new ArtistaModel
            {
                Nome = artistaDTO.Nome,
                NomeArtistico = artistaDTO.NomeArtistico,
                Idade = artistaDTO.Idade,
                PaisOrigem = artistaDTO.PaisOrigem
            };

            _dbContext.Artistas.Add(artista);

            _dbContext.SaveChanges();

            return Created("api/artistas",artista);
        }

        [HttpPut("{id}")]
        public ActionResult<ArtistaModel> Put([FromRoute] int id, [FromBody] ArtistaDTO artistaDTO)
        {

            var artista = _dbContext.Artistas.Find(id);

            if (artista == null) return NotFound(new RetornoComFalhaViewModel("Artista Não Encontrado"));

            artista.Nome = artistaDTO.Nome;
            artista.NomeArtistico = artistaDTO.NomeArtistico;
            artista.Idade = artistaDTO.Idade;
            artista.PaisOrigem = artistaDTO.PaisOrigem;

            _dbContext.SaveChanges();

            return Ok(artista);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {

            var artista = _dbContext.Artistas.Find(id);

            if (artista == null) return NotFound(new RetornoComFalhaViewModel("Artista Não Encontrado"));

            _dbContext.Artistas.Remove(artista);

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/album")]
        public ActionResult<AlbumModel> PostAlbum([FromRoute] int id, [FromBody] AlbumDTO albumDTO)
        {
            var artista = _dbContext.Artistas.Find(id);

            if (artista == null) return NotFound(new RetornoComFalhaViewModel("Artista Não Encontrado"));

            var album = new AlbumModel
            {
                Nome = albumDTO.Nome,
                AnoLancamento = albumDTO.AnoLancamento,
                CapaUrl = albumDTO.CapaUrl,
                ArtistaId = artista.Id,
                Musicas = albumDTO.Musicas?.Select(musicaDTO => new MusicaModel {
                    Nome = musicaDTO.Nome,
                    Duracao = musicaDTO.Duracao,
                    ArtistaId = artista.Id
                }).ToList()
            };

            _dbContext.Albuns.Add(album);

             _dbContext.SaveChanges();

            var viewModel = new AlbumComMusicasViewModel(album);

            return Created($"api/artista/{id}/album", viewModel);

        }
    }
}