using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicaComEF.API.Data;
using MusicaComEF.API.DTOs;
using MusicaComEF.API.Models;
using MusicaComEF.API.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicaComEF.API.Controllers
{
  
    [ApiController]
    [Route("api/playLists")]
    public class PlayListsController : ControllerBase
    {

        private readonly MusicasDbContext _dbContext;
        public PlayListsController(MusicasDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        public ActionResult<List<PlayListModel>> Get()
        {
            return Ok(_dbContext.Musicas.ToList());
        }

        [HttpGet("{id}/musica")]
        public ActionResult<List<MusicaModel>> GetMusicaId([FromRoute] int id)
        {
            var playListMusica = _dbContext.Musicas.Where(musicaDb => musicaDb.PlayListId == id).ToList();
          
            return Ok(playListMusica);

        }


        [HttpGet("{id}")]
        public ActionResult<PlayListModel> GetPorId([FromRoute] int id)
        {

            var playList = _dbContext.PlayLists.Find(id);

            if (playList == null) return NotFound(new RetornoComFalhaViewModel("PlayList Não Encontrada"));

            return Ok(playList);
        }


        [HttpPost]
        public ActionResult<PlayListModel> Post([FromBody] PlayListDTO playListDTO)
        {
            if (_dbContext.PlayLists.Any(playListDb => playListDb.Nome == playListDTO.Nome))
            {
                return BadRequest(new RetornoComFalhaViewModel("Já Contém Esta PlayList No Banco"));
            }

            var playList = new PlayListModel
            {
                Nome = playListDTO.Nome,
               Genero = playListDTO.Genero
            };

            _dbContext.PlayLists.Add(playList);

            _dbContext.SaveChanges();

            return Created("api/playLists", playList);
        }

      
        [HttpPut("{id}")]
        public ActionResult<PlayListModel> Put([FromRoute] int id, [FromBody] PlayListDTO playListDTO)
        {

            var playList = _dbContext.PlayLists.Find(id);

            if (playList == null) return NotFound(new RetornoComFalhaViewModel("PlayList Não Encontrada"));


            playList.Nome = playListDTO.Nome;
            playList.Genero = playListDTO.Genero;
           

            _dbContext.SaveChanges();

            return Ok(playList);
        }
            


            [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {

            var playList = _dbContext.PlayLists.Find(id);

            if (playList == null) return NotFound(new RetornoComFalhaViewModel("PlayList Não Encontrada"));


            _dbContext.PlayLists.Remove(playList);

            _dbContext.SaveChanges();

            return NoContent();
        }

       

        [HttpPost("{id}/musicas")]
        public ActionResult<MusicaModel> PostMusica([FromRoute] int id, [FromBody] PlayListMusicaDTO playListMusicaDTO)
        {
            var playListAtual = _dbContext.PlayLists.Find(id);
            var musica = _dbContext.Musicas.Find(playListMusicaDTO.MusicaId);

            if (playListAtual == null) return NotFound(new RetornoComFalhaViewModel("PlayList Não Encontrada"));

            playListAtual.Musicas.Add(musica);
              
           
            _dbContext.SaveChanges();

            var viewModel = new PlayListComMusicaViewModel(playListAtual);

            return Created($"api/playLists/{id}/musicas", viewModel);

        }

        [HttpDelete("{idPlayList}/musica/{idMusica}")]
        public ActionResult<PlayListModel> RemoverMusicaDaPlayList([FromRoute] int idPlayList, [FromRoute] int idMusica)
        {
            var playList = _dbContext.PlayLists.Find(idPlayList);

            var musica = _dbContext.Musicas.Find(idMusica);


            playList.Musicas.Remove(musica);

            _dbContext.SaveChanges();

            return Ok(new RetornoMensagemViewModel("Play List Removida da Musica"));
        
        }
    }
}
