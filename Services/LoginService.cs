using BankApi.controllers;
using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class LoginService
{
    public readonly DbBankContext _contex;
    public LoginService(DbBankContext contex)
    {
        _contex = contex;
    }

    public async Task<Administrator?> GetAdmin(AdminDto admin)
    {
        return await _contex.Administrator
        .SingleOrDefaultAsync(x => x.Email == admin.Email
        && x.Pwd == admin.Pwd);
    }
    public async Task<dynamic> GetAll()
    {
        return await _contex.Administrator.ToListAsync();     
    }
}