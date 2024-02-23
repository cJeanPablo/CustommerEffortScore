using AutoMapper;
using Moq;
using Ces.Api.Infrastructure.Context;
using Ces.Api.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Business.ViewModels;
using Ces.Api.Domain;

namespace Ces.Test.Units
{
    public class PerguntasTest
    {
        private readonly Mock<IPerguntaRepository> _perguntaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ITipoPerguntaDomain> _tipoPerguntaDomainMock;
        private readonly Mock<INotificador> _notificadorMock;

        private readonly PerguntaDomain _perguntaDomain;
        public PerguntasTest()
        {
            _perguntaRepositoryMock = new Mock<IPerguntaRepository>();
            _mapperMock = new Mock<IMapper>();
            _tipoPerguntaDomainMock = new Mock<ITipoPerguntaDomain>();
            _notificadorMock = new Mock<INotificador>();

            _perguntaDomain = new(_mapperMock.Object,
                    _tipoPerguntaDomainMock.Object,
                    _perguntaRepositoryMock.Object,
                    _notificadorMock.Object);
        }
       

        [Fact]
        public async Task BuscarPorId_Success()
        {

            _perguntaRepositoryMock.Setup(repo => repo.BuscarComCache(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<Expression<Func<Pergunta, bool>>>())).ReturnsAsync(new Pergunta[1]);
            var result = await _perguntaDomain.BuscarPorId(0);

            Assert.Single(result);
        }


        [Fact]
        public void ValidarSeDeveApresentarProximasPerguntas_Success()
        {
            var txtResposta = "3";
            var notaMinima = 5;

            var result = _perguntaDomain.ValidarSeDeveApresentarProximasPerguntas(txtResposta, notaMinima);

            Assert.True(result);
        }

        [Fact]
        public async Task MontaPerguntasComposicao_Success()
        {
            var result = await _perguntaDomain.MontaPerguntasComposicao();

            Assert.NotNull(result);
            Assert.IsType<List<PerguntaComposicaoViewModel>>(result);
        }

        [Fact]
        public void ValidarSeDeveApresentarProximasPerguntas_Failure()
        {
            var txtResposta = "3";
            var notaMinima = 2;

            var result = _perguntaDomain.ValidarSeDeveApresentarProximasPerguntas(txtResposta, notaMinima);

            Assert.False(result);
        }
        
        [Fact]
        public async Task ValidarSeDeveApresentarProximasPerguntas_InputInvalido()
        {
            var txtResposta = "teste";
            var notaMinima = 2;

            var result = _perguntaDomain.ValidarSeDeveApresentarProximasPerguntas(txtResposta, notaMinima);

            Assert.False(result);
        }
    }
}
