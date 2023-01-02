using RL.LeilaoOnline.Core;
using System;

namespace ConsoleApp2
{
    internal class Program
    {
        private static void Verifica (double esperado, double obtido)
        {
            if (esperado == obtido)
            { Console.WriteLine("Sucesso!"); }
            else
            { Console.WriteLine("Erro!"); }
        }

        private static void LeilaoComLances()
        {
            //arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);
        }

        private static void LeilaoComUmLance()
        {
            //arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 1000);

            //act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);

        }

        static void Main(string[] args)
        {
            LeilaoComLances();
            LeilaoComUmLance();
        }
    }
}
