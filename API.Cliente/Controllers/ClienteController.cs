using API.Cliente.Context;
using API.Cliente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Cliente.Controllers
{
    public class ClienteController : ControllerBase
    {
        private readonly ClienteContext _context;

        public ClienteController(ClienteContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/v1/cliente")]
        public async Task<ActionResult<ClienteModel>> GetCliente() 
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet]
        [Route("api/v1/cliente/{cpf}")]
        public async Task<ActionResult<ClienteModel>> GetClienteCpf(string cpf) 
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(i => i.Cpf == cpf);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        [Route("api/v1/cliente")]
        public async Task<ActionResult<ClienteModel>> PostCliente(ClienteModel cliente) 
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCliente), new { cpf = cliente.Cpf }, cliente);
        }

        [HttpPut]
        [Route("api/v1/cliente")]
        public async Task<ActionResult> PutCliente(string cpf, ClienteModel clienteAtualizado)
        {
            if(cpf != clienteAtualizado.Cpf)
            {
                return BadRequest();
            }

            _context.Entry(clienteAtualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(clienteAtualizado != null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }   
            return NoContent();
        }

        [HttpDelete]
        [Route("api/v1/cliente")]
        public async Task<ActionResult<ClienteModel>> DeleteCliente(string cpf) 
        { 
            var cliente = await _context.Clientes.FirstOrDefaultAsync(i => i.Cpf == cpf);

            if(cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
    }
}
