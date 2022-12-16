using BankApi.controllers;
using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class AccountTypeService{
     public readonly DbBankContext _contex;
    public AccountTypeService(DbBankContext contex)
    {
        _contex=contex;
    }

    public async Task<AccountType?>GetById(int id)
    {
        return await _contex.AccountTypes.FindAsync(id);
    }
}