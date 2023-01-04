using Microsoft.AspNetCore.Mvc;
using BankApi.Data;
using BankApi.Data.BankModels;
using BankApi.Services;

namespace BankApi.controllers;


[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    public readonly LoginService _loginService;
    public LoginController(LoginService _loginService)
    {
        this._loginService = _loginService;
    }

    [HttpPost]
    public async Task<dynamic> Login(AdminDto admin)
    {
        var adminDto = await _loginService.GetAdmin(admin);

        if (adminDto is null)
        {
            return BadRequest(new { message = "Credenciales incorrectas" });
        }

        return Ok(new { token = "some validation" });
    }


    [HttpGet]
    public async Task<dynamic?> Login()
    {
        return await _loginService.GetAll();
    }
}