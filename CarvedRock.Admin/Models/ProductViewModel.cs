using System.ComponentModel;
using CarvedRock.Admin.Data;

namespace CarvedRock.Admin.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    
    [DisplayName("Category")] 
    public string? CategoryName { get; set; }

    public static ProductViewModel FromProduct(Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId ?? 0,
            CategoryName = product.Category?.Name
        };
    }

    public Product ToProduct()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            IsActive = IsActive,
            CategoryId = CategoryId
        };

    }

}
