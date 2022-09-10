namespace MusicaComEF.API.Models
{
    public class AlbumModel
    {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public int AnoLancamento { get; set; }
        public string CapaUrl { get; set; }

        public int ArtistaId { get; set; }
        public virtual ArtistaModel Artista { get; set; }
        public virtual List<MusicaModel> Musicas { get; set; }
    }
}
