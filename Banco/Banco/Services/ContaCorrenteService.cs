using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco.Domain.Models;

namespace Banco.Services
{
    public interface IContaCorrenteService
    {
        IEnumerable<ContaCorrente> GetAll();

        IEnumerable<ContaCorrente> GetAllPaginated(int numberOfPages, int pageNumber);

        ContaCorrente Get(int id);

        void Insert(ContaCorrente cc);

        void Update(int id, ContaCorrente cc);

        void Delete(int id);
    }

    public class ContaCorrenteService : IContaCorrenteService
    {
        private LancamentosBancoDb _context;
        private AuditEventService _auditService;

        public ContaCorrenteService(LancamentosBancoDb context, AuditEventService auditService)
        {
            this._context = context;
            this._auditService = auditService;
        }

        public void Delete(int id)
        {
            try
            {
                var cc = Get(id);

                if (cc == null)
                {
                    throw new Exception("Conta Corrente not found to delete");
                }

                _context.ContaCorrentes.Remove(cc);

                _auditService.Save(new AuditEvent { CreatedDate = DateTime.Now, Action = "Delete", Entity = "CC", Id = id });
            }
            catch (AppException)
            {
                throw new AppException("CC cannot be deleted");
            }
        }

        public ContaCorrente Get(int id)
        {
            try
            {
                var cc = _context.ContaCorrentes.Find(id);

                return cc;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ContaCorrente> GetAll()
        {
            try
            {
                var cc = _context.ContaCorrentes.ToList();

                return cc;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ContaCorrente> GetAllPaginated(int numberOfObjectsPerPage, int pageNumber)
        {
            try
            {
                var cc = _context.ContaCorrentes.ToList();

                return cc
                    .Skip(numberOfObjectsPerPage * pageNumber)
                    .Take(numberOfObjectsPerPage); ;
            }
            catch (AppException ex)
            {
                throw ex;
            }
        }

        public void Insert(ContaCorrente cc)
        {
            _context.ContaCorrentes.Add(cc);
            _context.SaveChanges();

            _auditService.Save(new AuditEvent { CreatedDate = DateTime.Now, Action = "Create", Entity = "CC", Id = cc.IDConta });
        }

        public void Update(int id, ContaCorrente cc)
        {
            if (id != cc.IDConta)
            {
                throw new Exception("Cannot update with different ids");
            }

            _context.Entry(cc).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                _auditService.Save(new AuditEvent { CreatedDate = DateTime.Now, Action = "Update", Entity = "CC", Id = cc.IDConta });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Cannot find a cc to upload");
            }
        }
    }

}
