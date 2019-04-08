using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco.Domain.Models;

namespace Banco
{
    public class LancamentosBancoDb : DbContext
    {
        public LancamentosBancoDb(DbContextOptions options) : base(options) { }

        public DbSet<ContaCorrente> ContaCorrentes { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<TipoLancamento> TiposLancamento { get; set; }

    }
}
