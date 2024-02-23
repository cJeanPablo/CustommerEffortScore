using Ces.Api.Business.Models;

namespace Ces.Api.Business.Interfaces
{
    public interface ITipoPerguntaDomain
    {
        public Task<IEnumerable<TipoPergunta>> BuscarTipoPergunta(int idTipoPergunta);
        public Task<TipoPergunta[]> BuscarTiposPerguntaAtivos();
    }
}
