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
        return clientNotFound(id); 
       }
       return client;
    }

    [HttpPost]
    public  async Task<IActionResult> CrearClient(Client cliente)
    {
        var newclient=await _service.Create(cliente);
        return CreatedAtAction(nameof(GetClientById),new{id=newclient.Id},newclient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, Client client)
    {
        if(id!=client.Id){
            return BadRequest(new{
                message=$"El ID =({id}) de la url no coincide con el id ({client.Id}) del cuerpo de la solicitud"
                });            
        }

        var existeCliente=await _service.GetById(id);
        if(existeCliente is not null){
            await _service.Update(id,client);
            return NoContent();
        }
        else
        {
            return clientNotFound(id);
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
            return clientNotFound(id);
        }
           
    }

    public NotFoundObjectResult clientNotFound(int id)
    {
        return NotFound(new{message=$"El cliente con ID={id} no existe"});
    }

}