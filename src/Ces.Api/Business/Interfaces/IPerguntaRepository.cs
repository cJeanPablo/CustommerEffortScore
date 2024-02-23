using Ces.Api.Business.Models;

namespace Ces.Api.Business.Interfaces
{
    public interface IPerguntaRepository : IRepository<Pergunta>
    {
        Task<IEnumerable<Pergunta>> ObterPerguntasPorTipo(Guid idTipoPergunta);
    }
}
