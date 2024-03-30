using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Name {get; set;} = "";
    public string Description {get; set;} = "";
    public DateTime Date {get; set;}

    [Column(TypeName = "decimal(19, 4)")]
    [DisplayFormat(DataFormatString="{0:C}")]
    public decimal Amount {get; set;}
    
    public Category? Category {get; set;}
}