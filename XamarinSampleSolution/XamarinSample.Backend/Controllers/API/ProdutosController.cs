using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XamarinSample.Backend.Models;

namespace XamarinSample.Backend.Controllers.API
{
    [RoutePrefix("api/produtos")]
    public class ProdutosController : ApiController
    {
        [HttpPost]
        [Route("")]
        public async Task Save([FromBody]Produto model)
        {
            try
            {
                using (var _context = ApplicationDbContext.Create())
                {
                    var _current = await _context.Produtos.FindAsync(model.Id);

                    if (_current == null)
                        _context.Produtos.Add(model);
                    else
                    {
                        _current.Descricao = model.Descricao;
                        _current.Valor = model.Valor;
                    }

                    if (_context.ChangeTracker.HasChanges())
                        await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}