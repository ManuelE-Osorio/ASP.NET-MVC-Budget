using System;
using System.Collections.Generic;
using Microsoft.Identity.Client;

namespace Budget.Models;

public class IndexViewModel
{
    public List<Category> Categories {get; set;} = [];
    public List<Transaction> Transactions {get; set;} = [];
    public string? TransactionName { get; set; }
    public string? CategoryName { get; set; }
    public DateOnly Date {get; set;}
}