using CarvedRock.Admin.Models;

namespace CarvedRock.Admin.Logic;

public interface IProductLogic
{
    Task<List<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel?> GetProductById(int id);
    Task AddNewProduct(ProductViewModel productViewToAdd);
    Task RemoveProduct(int id);
    Task UpdateProduct(ProductViewModel productViewToUpdate);
}

