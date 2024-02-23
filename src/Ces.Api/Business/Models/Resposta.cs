namespace Ces.Api.Business.Models
{
    public class Resposta
    {
        public int? IdPergunta { get; set; }
        public string? TxtPergunta { get; set; }
        public string? TxtResposta { get; set; }
        public int? IdCompra { get; set; }

        public Resposta(int idPergunta, string? txtPergunta, string? txtResposta, int idCompra)
        {
            IdPergunta = idPergunta;
            TxtPergunta = txtPergunta;
            TxtResposta = txtResposta;
            IdCompra = idCompra;
        }
    }
}
