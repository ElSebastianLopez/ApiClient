using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankApi.Data.BankModels;

public partial class AccountDTOsOut
{
    public int Id { get; set; }

    public string AccountName { get; set; }

    public string ClientName { get; set; }

    public decimal Balance { get; set; }

    public DateTime RegDate { get; set; }

}

