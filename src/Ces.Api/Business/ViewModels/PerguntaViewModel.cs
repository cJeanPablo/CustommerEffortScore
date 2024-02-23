namespace Ces.Api.Business.ViewModels
{
    public class PerguntaViewModel
    {
        public int Id { get; set; }
        public int IdTipoPergunta { get; set; }
        public string? TxtPergunta { get; set; }
        public int? NotaMaxima { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }

    }
}
