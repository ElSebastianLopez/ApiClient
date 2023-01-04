using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankApi.Data.BankModels;

public partial class AdminDto
{
    public string Pwd { get; set; }

    public string Email { get; set; }

}