using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;

namespace Ces.Api.Domain
{
    public class TipoPerguntaDomain : ITipoPerguntaDomain
    {
        private readonly ITipoPerguntaRepository _tipoPerguntaRepository;

        public TipoPerguntaDomain(ITipoPerguntaRepository tipoPerguntaRepository)
        {
            _tipoPerguntaRepository = tipoPerguntaRepository;
        }
        public async Task<IEnumerable<TipoPergunta>> BuscarTipoPergunta(int idTipoPergunta)
        {
            var retorno = await _tipoPerguntaRepository.BuscarComCache($"TipoPerguntaCacheId{idTipoPergunta}", 1800, p => p.Id == idTipoPergunta && p.Ativo);
            return retorno;
        }


        public async Task<TipoPergunta[]> BuscarTiposPerguntaAtivos()
        {
            return await _tipoPerguntaRepository.BuscarComCache($"TipoPerguntaCache", 864000, p => p.Ativo);
        }

    }
}
