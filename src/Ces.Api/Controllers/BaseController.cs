using Azure;
using Ces.Api.Business.Interfaces;
using Ces.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ces.Api.Business.Notificacoes;

namespace Ces.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;

        }
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
        protected ActionResult CustomResponse(object? result = null)
        {
            ErrorResponse errorResponse = new();

            if (OperacaoValida())
            {
                if (result != null)
                    return Ok(result);

                return Ok();
            }
            else
            {
                errorResponse.Erro = true;
                errorResponse.Mensagens = _notificador.ObterNotificacoes();
            }

            errorResponse.CorrelationId = HttpContext?.TraceIdentifier;
            return StatusCode(ObterStatusCode(errorResponse.Mensagens), errorResponse);
        }
        public static int ObterStatusCode(IEnumerable<ErrorDetails> mensagens)
        {
            if (mensagens.Any(m => m.Tipo == ErrorType.Validação.ToString()))
                return 400;

            if (mensagens.Any(m => m.Tipo == ErrorType.Negócio.ToString()))
                return 422;

            if (mensagens.Any(m => m.Tipo == ErrorType.Integração.ToString()))
                return 502;

            return 500;
        }
        protected ActionResult CustomResponse(bool apresentarProximaPergunta, object? result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    apresentarProximaPergunta,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState, ErrorType type)
        {
            if (!modelState.IsValid)
            {
                NotificarErroModelInvalida(modelState, type);
            }
            return CustomResponse();


        }
        protected void NotificarErroModelInvalida(ModelStateDictionary modelState, ErrorType type)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg, type);
            }
        }

        protected void NotificarErro(string mensagem, ErrorType type)
        {
            _notificador.Handle(new ErrorDetails(type, mensagem));
        }

    }
}
