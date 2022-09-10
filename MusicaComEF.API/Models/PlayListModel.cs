namespace MusicaComEF.API.Models
{
    public class PlayListModel
    {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public virtual List<MusicaModel> Musicas { get; set; } = new();
       
    }
}
