using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Banco.Domain.Models;
using Banco.Services;


namespace Banco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {

        private ILancamentoService _lancamentoService;

        public LancamentoController(ILancamentoService service)
        {
            _lancamentoService = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Post api/values/5
        [HttpPost]
        public IActionResult Operacao([FromBody] ContaCorrente debito, [FromBody] ContaCorrente credito, [FromBody] decimal valor)
        {

            Lancamento lanc = new Lancamento();
            var ccDebito = _lancamentoService.GetCC(debito);
            var ccCredito = _lancamentoService.GetCC(credito);

            //Verifica se as Contas Existem
            if (ccDebito == null){

                return NotFound();
            }
            else if(ccCredito == null){

                return NotFound();
            }
            else{

                lanc.ContaOrigem = debito;
                lanc.ContaDestino = credito;
                lanc.Valor = valor;

                _lancamentoService.Insert(lanc);
                
            }

            return Ok(lanc);
        }



        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
