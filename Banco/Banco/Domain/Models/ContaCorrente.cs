using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Domain.Models
{
    public class ContaCorrente
    {
        public int IDConta { get; set; }
        public int NumeroBanco { get; set; }
        public int AgenciaConta { get; set; }
        public int NumeroConta { get; set; }
        public int DigitoConta { get; set; }
        public string CPFTitular { get; set; }
        public string CNPJTitular { get; set; }
        public string NomeTitular { get; set; }

        public ContaCorrente() { }

    }
}
