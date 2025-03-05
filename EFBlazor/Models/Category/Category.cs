using System.ComponentModel.DataAnnotations;

namespace EFBlazor.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];
}
