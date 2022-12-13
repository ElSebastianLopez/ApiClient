using Microsoft.EntityFrameworkCore;
using BankApi.Data;
using BankApi.Data.BankModels;

namespace BankApi.Services;

public class ClientService{
     public readonly DbBankContext _contex;
    public ClientService(DbBankContext contex)
    {
        _contex=contex;
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _contex.Clients.ToListAsync();         
    }

    public async Task<Client?> GetById(int id)
    {
        return await _contex.Clients.FindAsync(id);
    }

    public async Task<Client> Create(Client cliente)
    {
       await _contex.Clients.AddAsync(cliente);
       await _contex.SaveChangesAsync();
        return cliente;
    }

    public async Task Update(Client client)
    {
        var ExisteClient= await GetById(client.Id);
        if(ExisteClient is not null)
        {
            ExisteClient.Name=client.Name;
            ExisteClient.Email=client.Email;
            ExisteClient.PhoneNumber=client.PhoneNumber;

           await _contex.SaveChangesAsync();

        }
    }

   public async Task Delete(int id)
    {
        var existeCliente=await GetById(id);
        if(existeCliente is not null)
        {
            _contex.Clients.Remove(existeCliente);
            await _contex.SaveChangesAsync();
        }                         
    }

}