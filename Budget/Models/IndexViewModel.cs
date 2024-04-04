using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace Budget.Models;

public class IndexViewModel
{
    public List<Category> Categories {get; set;} = [];
    public List<Transaction> Transactions {get; set;} = [];

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? TransactionName { get; set; }
    public string? CategoryName { get; set; }
    public DateOnly TransactionDate {get; set;}
}