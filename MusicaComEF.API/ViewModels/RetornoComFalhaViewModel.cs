namespace MusicaComEF.API.ViewModels
{
    public class RetornoComFalhaViewModel
    {
        public string Falha { get; set; }


        public RetornoComFalhaViewModel(string falha)
        {
            Falha = falha;
        }

    }
}
