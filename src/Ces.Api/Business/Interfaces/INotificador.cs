using Ces.Api.Middleware;
using Ces.Api.Business.Notificacoes;

namespace Ces.Api.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<ErrorDetails> ObterNotificacoes();

        void Handle(ErrorDetails notificacao);
    }
}
