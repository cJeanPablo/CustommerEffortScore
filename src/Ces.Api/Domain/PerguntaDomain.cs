using AutoMapper;
using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Business.ViewModels;
using Ces.Api.Middleware;

namespace Ces.Api.Domain
{
    public class PerguntaDomain : IPerguntaDomain
    {
        private readonly IMapper _mapper;
        private readonly ITipoPerguntaDomain _tipoPerguntaDomain;

        private readonly IPerguntaRepository _perguntaRepository;
        private readonly INotificador _notificador;

        public PerguntaDomain(IMapper mapper,
            ITipoPerguntaDomain tipoPerguntaDomain,
            IPerguntaRepository perguntaRepository,
            INotificador notificador)
        {
            _mapper = mapper;
            _tipoPerguntaDomain = tipoPerguntaDomain;
            _perguntaRepository = perguntaRepository;
            _notificador = notificador;
        }

        public async Task<IEnumerable<Pergunta>> BuscarPorId(int Id)
        {
            var retorno = await _perguntaRepository.BuscarComCache($"PerguntaCacheId{Id}", 1800, p => p.Id == Id);
            return retorno;
        }

        public async Task<IEnumerable<PerguntaComposicaoViewModel>> MontaPerguntasComposicao()
        {
            var tipoPerguntas = _mapper.Map<IEnumerable<TipoPerguntaViewModel>>(await _tipoPerguntaDomain.BuscarTiposPerguntaAtivos());
            List<PerguntaComposicaoViewModel> _perguntaComposicaoViewModels = new();

            foreach (var tipopergunta in tipoPerguntas)
            {
                var perguntas = _mapper.Map<IEnumerable<PerguntaViewModel>>(await _perguntaRepository.BuscarComCache(
                                                                                                        $"PerguntasCacheIdTipo{tipopergunta.Id}",
                                                                                                        1800,
                                                                                                        p => p.Ativo && p.IdTipoPergunta == tipopergunta.Id));
                if (perguntas.Any())
                {
                    foreach (var pergunta in perguntas)
                    {
                        PerguntaComposicaoViewModel perguntaComposicao = new()
                        {
                            Perguntas = pergunta,
                            TipoPerguntas = tipopergunta
                        };
                        _perguntaComposicaoViewModels.Add(perguntaComposicao);
                    }

                }
            }
            return _perguntaComposicaoViewModels;

        }

        public bool ValidarSeDeveApresentarProximasPerguntas(string txtResposta, int? notaMinima)
        {
            var inputValido = int.TryParse(txtResposta, out var notaUsuario);

            if (!inputValido)
            {
                _notificador.Handle(new ErrorDetails(ErrorType.Validação, "O input informado não é valido para a pergunta do tipo nota."));
                return false;
            }
            if (notaUsuario >= notaMinima)
            {
                return false;
            }
            return true;
        }
    }
}
