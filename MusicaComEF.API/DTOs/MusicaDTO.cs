namespace MusicaComEF.API.DTOs
{
    public class MusicaDTO
    {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Duracao { get; set; }

        public int? AlbumId { get; set; }
        public int ArtistaId { get; set; }
        public int? PlayListId { get; set; }
    }
}
