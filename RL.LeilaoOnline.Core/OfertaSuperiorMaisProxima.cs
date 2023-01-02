using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public double ValorDestino { get; }

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        => leilao.Lances.DefaultIfEmpty(new Lance(null, 0))
            .Where(l => l.Valor > ValorDestino)
            .OrderBy(l => l.Valor)
            .FirstOrDefault();
        
    }
}
