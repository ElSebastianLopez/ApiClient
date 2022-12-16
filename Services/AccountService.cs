using BankApi.controllers;
using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;

public class AccountService{
     public readonly DbBankContext _contex;
    public AccountService(DbBankContext contex)
    {
        _contex=contex;
    }

    public async Task<IEnumerable<AccountDTOsOut>>GetAll()
    {
        return await _contex.Accounts.Select(account => new AccountDTOsOut 
        {
            Id = account.Id,
            AccountName = account.AccountTypeNavigation.Name,
            ClientName =account.Client.Name,
            Balance = account.Balance,
            RegDate = account.RegDate
        }).ToListAsync();         
    }

    public async Task<AccountDTOsOut?>GetDTOById(int id)
    {
        return await _contex.Accounts
        .Where(a => a.Id == id)
        .Select(account => new AccountDTOsOut 
        {
            Id = account.Id,
            AccountName = account.AccountTypeNavigation.Name,
            ClientName =account.Client.Name,
            Balance = account.Balance,
            RegDate = account.RegDate
        }).SingleOrDefaultAsync();         
    }

    public async Task<Account?>GetById(int id)
    {
        return await _contex.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(AccountDTOsIn account)
    {
        Account newAccont = new Account();

        newAccont.ClientId =  account.ClientId;
        newAccont.AccountType =  account.AccountType;
        newAccont.Balance =  account.Balance;

         _contex.Accounts.Add(newAccont);
        await _contex.SaveChangesAsync();
        return newAccont;
    }

    public  async Task Update(int id,AccountDTOsIn account)
    {
        var ExisteAccount=await GetById(id);
        if(ExisteAccount is not null)
        {
            ExisteAccount.ClientId=account.ClientId;
            ExisteAccount.AccountType=account.AccountType;
            ExisteAccount.Balance=account.Balance;

            await _contex.SaveChangesAsync();

        }
    }

   public async Task Delete(int id)
    {
        var existeAccount=await GetById(id);
        if(existeAccount is not null)
        {
            _contex.Accounts.Remove(existeAccount);
            await _contex.SaveChangesAsync();
        }                         
    }

    public static implicit operator AccountService(AccountController v)
    {
        throw new NotImplementedException();
    }
}