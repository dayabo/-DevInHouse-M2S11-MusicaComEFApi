using MusicaComEF.API.Models;

namespace MusicaComEF.API.ViewModels
{
    public class PlayListComMusicaViewModel
    {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Genero { get; set; }


        public virtual List<AlbumMusicaViewModel> Musicas { get; set; }

        public PlayListComMusicaViewModel(PlayListModel playList)
        {
            Id = playList.Id;
            Nome = playList.Nome;
            Genero = playList.Genero;
            Musicas = playList?.Musicas.Select(musica => new AlbumMusicaViewModel
            {
                Id = musica.Id,
                Nome = musica.Nome,
                Duracao = musica.Duracao
            }).ToList();
        }
    }
}
