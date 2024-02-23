using AutoMapper;
using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Business.ViewModels;
using Ces.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Ces.Api.Controllers
{
    [Route("api/v{version:apiVersion}/Ces")]
    [ApiVersion("1.0")]
    [ApiController]

    public class CesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRespostaDomain _respostaDomain;
        private readonly IPerguntaDomain _perguntaDomain;
        private readonly ITipoPerguntaDomain _tipoPerguntaDomain;
        private readonly IHttpContextAccessor _httpContext;
        public CesController(
            IMapper mapper,
            IRespostaDomain respostaDomain,
            INotificador notificador,
            IPerguntaDomain perguntaDomain,
            ITipoPerguntaDomain tipoPerguntaDomain,
            IHttpContextAccessor httpContext) : base(notificador)
        {

            _mapper = mapper;
            _respostaDomain = respostaDomain;
            _perguntaDomain = perguntaDomain;
            _tipoPerguntaDomain = tipoPerguntaDomain;
            _httpContext = httpContext;
        }

        [HttpGet]
        [Route("BuscarPerguntasComposicao")]
        public async Task<ActionResult<IEnumerable<PerguntaComposicaoViewModel>>> BuscaPerguntasComposicao()
        {
            var retorno = await _perguntaDomain.MontaPerguntasComposicao();
            return CustomResponse(retorno);
        }

        [HttpPost]
        [Route("IncluirRespostas")]
        public async Task<ActionResult<RespostasViewModel>> IncluirRespostas([FromBody] RespostasViewModel respostas)
        {
            if (!ModelState.IsValid) { return CustomResponse(ModelState); }

            var perguntas = _mapper.Map<IEnumerable<PerguntaViewModel>>(await _perguntaDomain.BuscarPorId(respostas.IdPergunta));
            var tipoPergunta = _mapper.Map<IEnumerable<TipoPerguntaViewModel>>(await _tipoPerguntaDomain.BuscarTipoPergunta(perguntas.First().IdTipoPergunta));

            respostas.TxtPergunta = perguntas.First().TxtPergunta;

            if (respostas.TxtResposta.IsNullOrEmpty())
            {
                await _respostaDomain.GravarLogResposta(_mapper.Map<Resposta>(respostas), tipoPergunta.FirstOrDefault()?.Tipo);
                return CustomResponse(respostas);
            }


            if (tipoPergunta.FirstOrDefault()?.Tipo == Business.Enums.TipoPergunta.Nota.ToString())
            {
                bool deveApresentarProximasPerguntas = _perguntaDomain.ValidarSeDeveApresentarProximasPerguntas(respostas.TxtResposta, perguntas.Where(p => p.IdTipoPergunta == tipoPergunta.FirstOrDefault()?.Id).First().NotaMaxima);
                if (OperacaoValida())
                {
                    await _respostaDomain.GravarLogResposta(_mapper.Map<Resposta>(respostas), tipoPergunta.FirstOrDefault()?.Tipo);
                }
                return CustomResponse(deveApresentarProximasPerguntas, respostas);
            }

            await _respostaDomain.GravarLogResposta(_mapper.Map<Resposta>(respostas), tipoPergunta.FirstOrDefault()?.Tipo);
            return CustomResponse(respostas);
        }

    }
}
