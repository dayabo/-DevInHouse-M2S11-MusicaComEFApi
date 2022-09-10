using System.ComponentModel.DataAnnotations;

namespace MusicaComEF.API.DTOs
{
    public class AlbumDTOPost
    {
        [Required(ErrorMessage = " O nome é obrigatório !")]
        public string Nome { get; set; }

        [Range(1500, 2022, ErrorMessage = " O ano de lançamento precisa ser válido !")]
        public int AnoLancamento { get; set; }
        public string CapaUrl { get; set; }

        [Required(ErrorMessage = " O artista é obrigatório !")]
        [Range(1, int.MaxValue, ErrorMessage = " O id do artista precisa ser válido !")]
        public int ArtistaId { get; set; }
    }
}
