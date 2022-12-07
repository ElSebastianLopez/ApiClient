using Microsoft.AspNetCore.Mvc;
using BankApi.Data;
using BankApi.Data.BankModels;

namespace BankApi.controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController:ControllerBase
{
    public readonly DbBankContext _contex;
    public ClienteController(DbBankContext context)
    {
        _contex=context;
        
    }

    [HttpGet]
    public IEnumerable<Client> GetClients()
    {
        return _contex.Clients.ToList();
    }
    
    [HttpGet("{id}")] 
    public ActionResult<Client> GetClientById( int id)
    {
       var client=_contex.Clients.Find(id);
       if(client is null)
       {
        return NotFound(); 
       }
       return client;
    }

    [HttpPost]
    public IActionResult CrearClient(Client cliente)
    {
        _contex.Clients.Add(cliente);
        _contex.SaveChanges();
        return CreatedAtAction(nameof(GetClientById),new{id=cliente.Id},cliente);
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, Client client)
    {
        if(id!=client.Id){
            return BadRequest();            
        }
        var existeCliente=_contex.Clients.Find(id);
        if(existeCliente is null){
            return NotFound();
        }
        existeCliente.Name=client.Name;
        existeCliente.Email=client.Email;
        existeCliente.PhoneNumber=client.PhoneNumber;
        _contex.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]    
    public IActionResult EliminarCliente(int id)
    {
        var existeCliente=_contex.Clients.Find(id);

        if(existeCliente is null)
        {
             return NotFound();
        }
        _contex.Clients.Remove(existeCliente);
        _contex.SaveChanges();

        return Ok();
           
    }

}

