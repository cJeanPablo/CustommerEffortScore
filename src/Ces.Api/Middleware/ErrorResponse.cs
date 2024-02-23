namespace Ces.Api.Middleware
{
    public class ErrorResponse
    {

        public bool Erro { get; set; } = false;
        public List<ErrorDetails> Mensagens { get; set; }
        public string CorrelationId { get; set; }

        public ErrorResponse()
        {
            Mensagens = new List<ErrorDetails>();
            CorrelationId = string.Empty;
        }

        public ErrorResponse(ErrorType tipo, string mensagem, string correlationId)
        {
            Erro = true;
            CorrelationId = correlationId;
            Mensagens = new List<ErrorDetails>();
            AddError(tipo, mensagem);
        }

        public void AddError(ErrorType tipo, string mensagem)
            => Mensagens.Add(new ErrorDetails(tipo, mensagem));
    }
}