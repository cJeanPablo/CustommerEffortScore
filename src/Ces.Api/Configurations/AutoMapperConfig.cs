using AutoMapper;
using Ces.Api.Business.Models;
using Ces.Api.Business.ViewModels;

namespace Ces.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<Pergunta, PerguntaViewModel>().ReverseMap();
            CreateMap<TipoPergunta, TipoPerguntaViewModel>().ReverseMap();
            CreateMap<RespostasViewModel, Resposta>().ConstructUsing(r =>
                new Resposta(r.IdPergunta, r.TxtPergunta, r.TxtResposta, r.IdCompra));


        }
    }
}
