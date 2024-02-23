namespace Ces.Api.Business.Models
{
    public class Pergunta : Entity
    {
        public int IdTipoPergunta { get; set; }
        public string? TxtPergunta { get; set; }
        public int? NotaMaxima { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }

        /*EF Relacionamento*/
        public TipoPergunta? TipoPergunta { get; set; }
    }

}
