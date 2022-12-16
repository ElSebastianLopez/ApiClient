using Microsoft.AspNetCore.Mvc;
using BankApi.Data;
using BankApi.Data.BankModels;
using BankApi.Services;

namespace BankApi.controllers;


[ApiController]
[Route("[controller]")]
public class AccountController:ControllerBase
{
    public readonly AccountService _accountService;
    public readonly ClientService _clienService;
    public readonly AccountTypeService _accountTypeservice;
    public AccountController(AccountService _accountService,
                                ClientService _clienService,
                                AccountTypeService _accountTypeservice)
    {
        this._accountService = _accountService;
        this._clienService = _clienService;
        this._accountTypeservice = _accountTypeservice;
    }

    [HttpGet]
    public async Task<IEnumerable<AccountDTOsOut>> GetAccounts()
    {
        return await _accountService.GetAll();
    }
    
    [HttpGet("{id}")] 
    public async Task<ActionResult<AccountDTOsOut>> GetAccountById( int id)
    {
       var  account=await _accountService.GetDTOById(id);
       if(account is null)
       {
        return accountNotFound(id); 
       }
       return account;
    }

    [HttpPost]
    public  async Task<IActionResult> Crear(AccountDTOsIn account)
    {
        var validationResul = await ValidateAccount(account);

        if(!validationResul.Equals("Valid")){
            return BadRequest(new {message = validationResul });
        }
        var newAccount=await _accountService.Create(account);
        return CreatedAtAction(nameof(GetAccountById),new{id=newAccount.Id},newAccount);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, AccountDTOsIn account)
    {
        if(id!=account.Id){
            return BadRequest(new{
                message=$"El ID =({id}) de la url no coincide con el id ({account.Id}) del cuerpo de la solicitud"
                });            
        }

        var existeAccount=await _accountService.GetById(id);
        if(existeAccount is not null){
            var validationResul = await ValidateAccount(account);

            if(!validationResul.Equals("Valid")){
                return BadRequest(new {message = validationResul });
            }
            await _accountService.Update(id,account);
            return NoContent();
        }
        else
        {
            return accountNotFound(id);
        }
    }

    [HttpDelete("{id}")]    
    public async Task<IActionResult> EliminarAccount(int id)
    {
        var existeAccount=await _accountService.GetById(id);

        if(existeAccount is not null)
        {
            await _accountService.Delete(id);
            return Ok();
        }
        else
        {
            return accountNotFound(id);
        }
           
    }

    public NotFoundObjectResult accountNotFound(int id)
    {
        return NotFound(new{message=$"El cliente con ID={id} no existe"});
    }

    public async Task<string> ValidateAccount(AccountDTOsIn account){
        string result = "Valid";

        var accounttype = await _accountTypeservice.GetById(account.AccountType);

        if(accounttype is null)
            result = $"el tipo de cuenta ({account.AccountType}) no existe";

        var clientId = account.ClientId.GetValueOrDefault();

        var client = await _clienService.GetById(clientId);

         if(client is null)
            result = $"el tipo de cliente ({clientId}) no existe";

        return result;
    }

}