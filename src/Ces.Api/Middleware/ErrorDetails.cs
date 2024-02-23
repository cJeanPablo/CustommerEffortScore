namespace Ces.Api.Middleware
{
    public class ErrorDetails
    {
        public string Mensagem { get; private set; }

        public string Tipo { get; private set; }

        public ErrorDetails(ErrorType tipo, string mensagem)
        {
            Tipo = tipo.ToString();
            Mensagem = mensagem;
        }
    }
}