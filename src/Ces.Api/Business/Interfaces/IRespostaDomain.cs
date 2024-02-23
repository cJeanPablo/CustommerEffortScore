using Ces.Api.Business.Models;

namespace Ces.Api.Business.Interfaces
{
    public interface IRespostaDomain
    {
        public Task<bool> GravarLogResposta(Resposta resposta, string tipoPergunta);
    }
}
