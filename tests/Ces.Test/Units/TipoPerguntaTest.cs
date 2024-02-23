using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Domain;
using Moq;
using System.Linq.Expressions;

namespace Ces.Test.Units
{
    public class TipoPerguntaTest
    {
        private readonly Mock<ITipoPerguntaRepository> _tipoPerguntaRepositoryMock;

        private readonly TipoPerguntaDomain _tipoPerguntaDomain;

        public TipoPerguntaTest()
        {
            _tipoPerguntaRepositoryMock = new Mock<ITipoPerguntaRepository>();

            _tipoPerguntaDomain = new(_tipoPerguntaRepositoryMock.Object);
        }

        [Fact]
        public async Task BuscarTipoPergunta_DeveRetornarTipoPerguntaAtivoPorId()
        {
            var tipoPergunta = new TipoPergunta
            {
                Id = 0,
                Ativo = true
            };

            _tipoPerguntaRepositoryMock
                .Setup(repo => repo.BuscarComCache($"TipoPerguntaCacheId{tipoPergunta.Id}", 1800, It.IsAny<Expression<Func<TipoPergunta, bool>>>()))
                .ReturnsAsync(new TipoPergunta[] { tipoPergunta });

            var result = await _tipoPerguntaDomain.BuscarTipoPergunta(tipoPergunta.Id);

            Assert.Single(result);
            Assert.Equal(tipoPergunta.Id, result.First().Id);
            Assert.True(result.First().Ativo);
        }

        [Fact]
        public async Task BuscarTiposPerguntaAtivos_DeveRetornarTiposPerguntaAtivos()
        {
            var tiposPerguntaAtivos = new TipoPergunta[2]
            {
                new TipoPergunta { Id = 0, Ativo = true },
                new TipoPergunta { Id = 0, Ativo = true }
            };

            _tipoPerguntaRepositoryMock
                .Setup(repo => repo.BuscarComCache($"TipoPerguntaCache", 864000, It.IsAny<Expression<Func<TipoPergunta, bool>>>()))
                .ReturnsAsync(tiposPerguntaAtivos);

            var result = await _tipoPerguntaDomain.BuscarTiposPerguntaAtivos();

            Assert.Equal(2, result.Count());
            Assert.All(result, tipo => Assert.True(tipo.Ativo));
        }

        [Fact]
        public async Task BuscarTipoPergunta_DeveRetornarListaVaziaQuandoNaoEncontrarTipoPergunta()
        {
            var tipoPerguntaId = 0;

            _tipoPerguntaRepositoryMock
                .Setup(repo => repo.BuscarComCache($"TipoPerguntaCacheId{tipoPerguntaId}", 1800, It.IsAny<Expression<Func<TipoPergunta, bool>>>()))
                .ReturnsAsync(new TipoPergunta[0]);

            var result = await _tipoPerguntaDomain.BuscarTipoPergunta(tipoPerguntaId);

            Assert.Empty(result);
        }

        [Fact]
        public async Task BuscarTiposPerguntaAtivos_DeveRetornarListaVaziaQuandoNaoEncontrarTiposPerguntaAtivos()
        {

            _tipoPerguntaRepositoryMock
                .Setup(repo => repo.BuscarComCache($"TipoPerguntaCache", 1800, It.IsAny<Expression<Func<TipoPergunta, bool>>>()))
                .ReturnsAsync(new TipoPergunta[0]);

            var result = await _tipoPerguntaDomain.BuscarTiposPerguntaAtivos();

            Assert.Empty(result);
        }
    }
}

