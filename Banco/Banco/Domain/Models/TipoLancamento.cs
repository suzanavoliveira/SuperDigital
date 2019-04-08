using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Domain.Models
{
    public class TipoLancamento
    {
        public int IDTipo { get; set; }
        public string DescricaoTipo { get; set; }
        public decimal Taxa { get; set; }

    }
}
