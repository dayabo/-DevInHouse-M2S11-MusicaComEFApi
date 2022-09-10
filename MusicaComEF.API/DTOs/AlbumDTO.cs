using MusicaComEF.API.Models;

namespace MusicaComEF.API.DTOs
{
    public class AlbumDTO
    {
        public string Nome { get; set; }
        public int AnoLancamento { get; set; }
        public string CapaUrl { get; set; }
        public virtual List<MusicaAlbumDTO> Musicas { get; set; }
    }
}
