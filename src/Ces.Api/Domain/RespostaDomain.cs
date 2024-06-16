using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Utilities.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Ces.Api.Domain
{
    public class RespostaDomain : IRespostaDomain
    {
        private readonly ILogger<Resposta> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public RespostaDomain(ILogger<Resposta> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
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
            EnviarEmailAgradecimento();

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
        public void EnviarEmailAgradecimento()
            {
            string remetenteEmail = "tccimpactaces@outlook.com"; 
            string senha = "tccimpacta123"; 
            string nomeRemetente = "Tcc Impacta";

            string destinatarioEmail = _contextAccessor.HttpContext.Request.Headers["Email"];
            string assunto = "Obrigado por nos avaliar!";
            string corpoMensagem = "Obrigado por deixar sua avaliação em nosso site, aqui está um cupom de 10% de desconto para a sua próxima compra, válido até 15 dias após o envio deste email. Cupom BEMVINDO10";

            // Configurações do servidor SMTP da Microsoft
            string smtpHost = "smtp-mail.outlook.com";
            int smtpPort = 587;
            bool enableSSL = true;

            // Criação do objeto de mensagem
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(remetenteEmail, nomeRemetente);
            mensagem.To.Add(destinatarioEmail);
            mensagem.Subject = assunto;
            mensagem.Body = corpoMensagem;
            mensagem.IsBodyHtml = false; // true se o corpo for HTML

            // Configuração do cliente SMTP
            SmtpClient smtpClient = new SmtpClient(smtpHost);
            smtpClient.Port = smtpPort;
            smtpClient.Credentials = new NetworkCredential(remetenteEmail, senha);
            smtpClient.EnableSsl = enableSSL;

            try
            {
                // Envio do e-mail
                smtpClient.Send(mensagem);
                Console.WriteLine("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao enviar o e-mail: " + ex.Message);
            }
        }
    }
}
