using Microsoft.AspNetCore.Mvc;
using BankApi.Data;
using BankApi.Data.BankModels;
using BankApi.Services;


namespace BankApi.controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController:ControllerBase
{
    public readonly ClientService _service;
    public ClienteController(ClientService service)
    {
        _service=service;
        
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> GetClients()
    {
        return await _service.GetAll();
    }
    
    [HttpGet("{id}")] 
    public async Task<ActionResult<Client>> GetClientById( int id)
    {
       var client=await _service.GetById(id);
       if(client is null)
       {
        return NotFound(); 
       }
       return client;
    }

    [HttpPost]
    public async Task<IActionResult> CrearClient(Client cliente)
    {
        var newclient=await _service.Create(cliente);
        return CreatedAtAction(nameof(GetClientById),new{id=newclient.Id},newclient);
    }

    [HttpPut()]
    public async Task<IActionResult> Actualizar( Client client)
    {
       
        var existeCliente=await _service.GetById(client.Id);
        if(existeCliente is not null){
            await _service.Update(client);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]    
    public async Task<IActionResult> EliminarCliente(int id)
    {
        var existeCliente=await _service.GetById(id);

        if(existeCliente is not null)
        {
           await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
           
    }

}