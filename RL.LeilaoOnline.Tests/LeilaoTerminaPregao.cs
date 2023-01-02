using RL.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RL.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] {800, 1150, 1400, 1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino,
            double valorEsperado,
            double[] ofertas
            )
        {
            //arranje - cenário
            var modalidade = new OfertaSuperiorMaisProxima(valorDestino);

            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var beltrano = new Interessada("beltrano", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                { leilao.RecebeLance(fulano, valor); }
                else
                { leilao.RecebeLance(beltrano, valor); }
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(800, new double[] { 800, 600, 400, 300 })]
        [InlineData(1200, new double[] { 200, 100, 1200, 500 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(
            double valorEsperado, double[] ofertas)
        {
            //arranje - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var beltrano = new Interessada("beltrano", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                { leilao.RecebeLance(fulano, valor); }
                else
                { leilao.RecebeLance(beltrano, valor); }
            }
            //foreach (var oferta in ofertas)
            //{
            //    leilao.RecebeLance(fulano, oferta);
            //}

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            //Assert
            var excessaoObtida = Assert.Throws<InvalidOperationException>(
                //act - método sob teste
                () => leilao.TerminaPregao()
            );

            var msgEsperada = "Não é possivel terminar o pregão sem ter sido inicializado um novo pregão.";
            Assert.Equal(msgEsperada, excessaoObtida.Message);
        }


        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            //arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            //act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }
    }
}
