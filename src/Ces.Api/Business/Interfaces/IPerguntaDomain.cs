using Ces.Api.Business.Models;
using Ces.Api.Business.ViewModels;
using System.Runtime.InteropServices;

namespace Ces.Api.Business.Interfaces
{
    public interface IPerguntaDomain
    {
        Task<IEnumerable<Pergunta>> BuscarPorId(int Id);
        Task<IEnumerable<PerguntaComposicaoViewModel>> MontaPerguntasComposicao();
        bool ValidarSeDeveApresentarProximasPerguntas(string? txtResposta, int? notaMinima);

    }
}
