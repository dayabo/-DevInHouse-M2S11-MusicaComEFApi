
namespace MusicaComEF.API.Models
{
    public class ArtistaModel
    {
        public int Id { get; internal set; }

        public string Nome { get; set; }
        public string NomeArtistico { get; set; }
        public int Idade { get; set; }
        public string PaisOrigem { get; set; }

        public virtual List<MusicaModel> Musicas { get; set; }
        public virtual List<AlbumModel> Albuns { get; set; }
    }
}

