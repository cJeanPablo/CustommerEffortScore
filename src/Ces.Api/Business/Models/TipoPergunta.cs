namespace Ces.Api.Business.Models
{
    public class TipoPergunta : Entity
    {
        public string? Tipo { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Pergunta>? Perguntas { get; set; }
    }

}
