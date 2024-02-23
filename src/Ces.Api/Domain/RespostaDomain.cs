using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Utilities.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Ces.Api.Domain
{
    public class RespostaDomain : IRespostaDomain
    {
        private readonly ILogger<Resposta> _logger;

        public RespostaDomain(ILogger<Resposta> logger)
        {
            _logger = logger;
        }

        public async Task<bool> GravarLogResposta(Resposta resposta, string tipoPergunta)
        {
            int respostaNota = 0;
            string respostaFinal = string.Empty;

            switch (tipoPergunta)
            {
                case "Nota":
                    respostaFinal = resposta.TxtResposta;
                    respostaNota = Convert.ToInt32(resposta.TxtResposta);
                    break;
                case "Normal":
                    respostaFinal = RemoverEspacosEQuebraDeLinha(resposta.TxtResposta);
                    if (RespostaInelegivel(respostaFinal))
                    {
                        _logger.LogInformation("Resposta inelegível - CES {@CES}", new
                        {
                            resposta = respostaFinal,
                            pergunta = resposta.TxtPergunta,
                            resposta.IdCompra,
                            TipoPergunta = tipoPergunta
                        });
                        return await Task.FromResult(false);
                    }
                    break;
            }

            _logger.LogInformation("LOG Gravação de resposta - CES {@CES}", new
            {
                resposta = respostaFinal,
                respostaNota,
                pergunta = resposta.TxtPergunta,
                resposta.IdCompra,
                TipoPergunta = tipoPergunta
            });

            return await Task.FromResult(true);
        }

        private bool RespostaInelegivel(string? txtResposta)
        {
            if (txtResposta.ContainsText())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string RemoverEspacosEQuebraDeLinha(string? txtResposta)
        {
            if (txtResposta.IsNullOrEmpty())
            {
                return string.Empty;
            }
            else
            {
                return txtResposta.Replace("\n", "").Replace("\r", "").Replace("  ", " ").Trim();
            }
        }
    }
}
