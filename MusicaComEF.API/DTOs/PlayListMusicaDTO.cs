using System.ComponentModel.DataAnnotations;

namespace MusicaComEF.API.DTOs
{
    public class PlayListMusicaDTO
    {

        [Required(ErrorMessage = "iD da musica é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = " O id do album precisa ser válido !")]
        public int MusicaId { get; set; }
       
    }
}
