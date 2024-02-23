using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ces.Api.Infrastructure.Repository
{
    public class PerguntaRepository : Repository<Pergunta>, IPerguntaRepository
    {
        public PerguntaRepository(CesContext cesContext, IMemoryCache cache) : base(cesContext, cache)
        {
        }

        public async Task<IEnumerable<Pergunta>> ObterPerguntasPorTipo(Guid idTipoPergunta)
        {
            return await Db.Perguntas.AsNoTracking().Include(tp => tp.TipoPergunta)
                 .ToListAsync();
        }
    }
}
