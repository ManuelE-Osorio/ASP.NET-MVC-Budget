using System.Collections.Generic;

namespace Budget.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set;} = "";
    public List<Transaction> Transactions {get; set;} = [];
}