using RL.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RL.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //arranje
            var valorNegativo = -100;

            //assert
            Assert.Throws<ArgumentException>(
                () => new Lance(null, valorNegativo)
                );
        }
    }
}
