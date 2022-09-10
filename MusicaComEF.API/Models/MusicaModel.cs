namespace MusicaComEF.API.Models
{
    public class MusicaModel
    {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Duracao { get; set; }

        public int? AlbumId { get; set; }
        public int ArtistaId { get; set; }
        public int? PlayListId { get; set; }
        public virtual AlbumModel Album { get; set; }
        public virtual ArtistaModel Artista { get; set; }
        public virtual PlayListModel PlayList { get; set; }

    }
}
