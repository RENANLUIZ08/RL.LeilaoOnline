using RL.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RL.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 100, 1200, 1400, 1600 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesMantemQuantidadeLancesQuandoJaFinalizado
            (int qtdEsperada, double[] ofertas)
        {
            //arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var beltrano = new Interessada("beltrano", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i%2) == 0)
                { leilao.RecebeLance(fulano, valor); }
                else 
                { leilao.RecebeLance(beltrano, valor); }
            }

            //foreach (var oferta in ofertas)
            //{ leilao.RecebeLance(fulano, oferta); }

            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdObtida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdObtida);
        }

        [Fact]
        public void NaoAceitaProximoLanceComUltimoCliente()
        {
            //arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act
            leilao.RecebeLance(fulano, 800);

            //Assert
            var qtdEsperada = 1;
            var qtdObtida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdObtida);
        }
    }
}
