using System.ComponentModel.DataAnnotations;

namespace MusicaComEF.API.DTOs
{
    public class ArtistaDTO
    {
        [Required (ErrorMessage = "o Nome é Obrigatório")]
        public string Nome { get; set; }
        public string NomeArtistico { get; set; }
        public int Idade { get; set; }
        public string PaisOrigem { get; set; }

    }
}
