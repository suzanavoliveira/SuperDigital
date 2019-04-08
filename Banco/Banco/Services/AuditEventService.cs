using Banco;
using Banco.Domain.Models;

namespace Banco.Services
{
    public interface IAuditEventService
    {
        void Save(AuditEvent auditEvent);
    }

    public class AuditEventService : IAuditEventService
    {
        private LancamentosBancoDb _context;

        public AuditEventService(LancamentosBancoDb context)
        {
            this._context = context;
        }

        public void Save(AuditEvent auditEvent)
        {
            _context.AuditEvents.Add(auditEvent);
            _context.SaveChanges();
        }
    }
}
