using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Domain.Models
{
    public class Lancamento
    {
        public int ID { get; set; }
        public TipoLancamento TipoLancamento { get; set; }
        public ContaCorrente ContaOrigem { get; set; }
        public ContaCorrente ContaDestino { get; set; }
        public decimal Valor { get; set; }

    }
}
