namespace MusicaComEF.API.ViewModels
{
    public class RetornoMensagemViewModel
    {
        public string Msg{ get; set; }


        public RetornoMensagemViewModel(string msg)
        {
            Msg = msg;
        }
    }
}
