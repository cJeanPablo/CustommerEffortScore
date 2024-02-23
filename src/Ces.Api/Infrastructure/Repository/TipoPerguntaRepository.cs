using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Infrastructure.Context;
using Microsoft.Extensions.Caching.Memory;

namespace Ces.Api.Infrastructure.Repository
{
    public class TipoPerguntaRepository : Repository<TipoPergunta>, ITipoPerguntaRepository
    {
        public TipoPerguntaRepository(CesContext cesContext, IMemoryCache cache) : base(cesContext, cache)
        {
        }


    }
}
