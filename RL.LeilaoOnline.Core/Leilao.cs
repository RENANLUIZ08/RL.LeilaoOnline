using System;
using System.Collections.Generic;
using System.Linq;

namespace RL.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoEstadoInicio,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private IList<Lance> _lances;
        private Interessada _ultimoCliente;
        private IModalidadeAvaliacao _avaliador;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Status { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Status = EstadoLeilao.LeilaoEstadoInicio;
            _avaliador = avaliador;
        }

        private bool NovoLanceEhLanceAceito(Interessada cliente, double valor)
        => ((Status == EstadoLeilao.LeilaoEmAndamento) &&
           (cliente != _ultimoCliente));



        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Status = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Status != EstadoLeilao.LeilaoEmAndamento)
            { throw new InvalidOperationException("Não é possivel terminar o pregão sem ter sido inicializado um novo pregão."); }
            Ganhador = _avaliador.Avalia(this);
            Status = EstadoLeilao.LeilaoFinalizado;
        }
    }
}