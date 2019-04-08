using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco.Domain.Models;

namespace Banco.Services
{
    public interface ILancamentoService
    {
        IEnumerable<Lancamento> GetAll();

        IEnumerable<Lancamento> GetAllPaginated(int numberOfPages, int pageNumber);

        Lancamento Get(int id);

        ContaCorrente GetCC(ContaCorrente cc);

        void Insert(Lancamento lanc);

        void Update(int id, Lancamento lanc);

        void Delete(int id);
    }

    public class LancamentoService : ILancamentoService
    {
        private LancamentosBancoDb _context;
        private AuditEventService _auditService;

        public LancamentoService(LancamentosBancoDb context, AuditEventService auditService)
        {
            this._context = context;
            this._auditService = auditService;
        }

        public void Delete(int id)
        {
            try
            {
                var lancamento = Get(id);

                if (lancamento == null)
                {
                    throw new Exception("Lancamento not found to delete");
                }

                _context.Lancamentos.Remove(lancamento);

                _auditService.Save(new AuditEvent { CreatedDate = DateTime.Now, Action = "Delete", Entity = "Lancamento", Id = id });
            }
            catch (AppException)
            {
                throw new AppException("Lancamento cannot be deleted");
            }
        }

        public Lancamento Get(int id)
        {
            try
            {
                var lancamento = _context.Lancamentos.Find(id);

                return lancamento;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public ContaCorrente GetCC(ContaCorrente cc)
        {
            try
            {
                var cc = _context.ContaCorrentes.Find(cc);

                return cc;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lancamento> GetAll()
        {
            try
            {
                var lancamento = _context.Lancamentos.ToList();

                return lancamento;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lancamento> GetAllPaginated(int numberOfObjectsPerPage, int pageNumber)
        {
            try
            {
                var cc = _context.Lancamentos.ToList();

                return cc
                    .Skip(numberOfObjectsPerPage * pageNumber)
                    .Take(numberOfObjectsPerPage); ;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public void Insert(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            _context.SaveChanges();

            _auditService.Save(new AuditEvent { CreatedDate = DateTime.Now, Action = "Create", Entity = "Lancamento", Id = cc.Id });
        }

        public void Update(int id, Lancamento lancamento)
        {
            if (id != lancamento.Id)
            {
                throw new Exception("Cannot update with different ids");
            }

            _context.Entry(lancamento).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                _auditService.Save(new AuditEvent { CreatedDate = DateTime.Now, Action = "Update", Entity = "lancamento", Id = lancamento.ID });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Cannot find a lancamento to upload");
            }
        }
    }

}
