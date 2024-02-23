using Ces.Api.Business.Interfaces;
using Ces.Api.Middleware;

namespace Ces.Api.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<ErrorDetails> _notificacoes;
        public Notificador()
        {
            _notificacoes = new List<ErrorDetails>();
        }
        public void Handle(ErrorDetails notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<ErrorDetails> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
