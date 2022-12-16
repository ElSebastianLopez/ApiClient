using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankApi.Data.BankModels;

public partial class AccountDTOsIn
{
    public int Id { get; set; }

    public int AccountType { get; set; }

    public int? ClientId { get; set; }

    public decimal Balance { get; set; }

}

