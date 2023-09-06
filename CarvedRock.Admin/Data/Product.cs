using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Admin.Data;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [DisplayName("Product Name")]
    public string? Name { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    [DataType(DataType.Currency)]
    [Range(0.01, 1000.00, ErrorMessage = "Value for {0} must be between " +
                                         "{1:C} and {2:C}")]
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}