using Microsoft.AspNetCore.Mvc;
using MusicaComEF.API.Data;
using MusicaComEF.API.DTOs;
using MusicaComEF.API.Models;
using MusicaComEF.API.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicaComEF.API.Controllers
{
    
    [ApiController]
    [Route("api/albuns")]
    public class AlbunsController : ControllerBase
    {

        private readonly MusicasDbContext _dbContext;


        public AlbunsController(MusicasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<AlbumModel>> Get()
        {
            return _dbContext.Albuns.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<AlbumModel> GetPorId([FromRoute] int id)
        {

            var album = _dbContext.Albuns.Find(id);

            if (album == null) return NotFound(new RetornoComFalhaViewModel("Album Não Encontrado"));

            return Ok(album);
        }

        [HttpPost]
        public ActionResult<AlbumModel> Post([FromBody] AlbumDTOPost albumDTOPost)
        {
            if (_dbContext.Albuns.Any(albumDb => albumDb.Nome == albumDTOPost.Nome))
            {
                return BadRequest(new RetornoComFalhaViewModel("Já Contém Este Album No Banco"));
            }

            var album = new AlbumModel
            {
                Nome = albumDTOPost.Nome,
                AnoLancamento = albumDTOPost.AnoLancamento,
                CapaUrl = albumDTOPost.CapaUrl,
                ArtistaId = albumDTOPost.ArtistaId
        };
           
            _dbContext.Albuns.Add(album);

            _dbContext.SaveChanges();

            return Created("api/albuns", album);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult<AlbumModel> Put([FromRoute] int id, [FromBody] AlbumDTOPost albumDTOPost)
        {
          
            var album = _dbContext.Albuns.Find(id);

            if (album == null)
            {
                return NotFound(new RetornoComFalhaViewModel("Album Não Encontrado"));

            }
            
                album.Nome = albumDTOPost.Nome;
                album.AnoLancamento = albumDTOPost.AnoLancamento;
                album.CapaUrl = albumDTOPost.CapaUrl;
                album.ArtistaId = albumDTOPost.ArtistaId;


                _dbContext.SaveChanges();

                return Ok(album);
            
            }

            // DELETE api/<ValuesController>/5
            [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {

            var album = _dbContext.Albuns.Find(id);

            if (album == null) return NotFound(new RetornoComFalhaViewModel("Album Não Encontrado"));

            _dbContext.Albuns.Remove(album);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
