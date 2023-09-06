using CarvedRock.Admin.Models;
using CarvedRock.Admin.Repository;

namespace CarvedRock.Admin.Logic;

public class ProductLogic : IProductLogic
{
    private readonly ICarvedRockRepository _repo;

    public ProductLogic(ICarvedRockRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProductViewModel>> GetAllProducts()
    {
        var products = await _repo.GetAllProductsAsync();

        // converts products from DB to productView models
        return products.Select(ProductViewModel.FromProduct).ToList();

        // the above is more terse syntax for:
        //var models = new List<ProductViewModel>();
        //foreach (var productView in products)
        //{
        //    models.Add(ProductViewModel.FromProduct(productView));
        //}
        //return models;
    }

    public async Task<ProductViewModel?> GetProductById(int id)
    {
        var product = await _repo.GetProductByIdAsync(id);
        return product == null ? null : ProductViewModel.FromProduct(product);
    }

    public async Task AddNewProduct(ProductViewModel productViewToAdd)
    {
        var productToSave = productViewToAdd.ToProduct();
        await _repo.AddProductAsync(productToSave);
    }

    public async Task RemoveProduct(int id)
    {
        await _repo.RemoveProductAsync(id);
    }

    public async Task UpdateProduct(ProductViewModel productViewToUpdate)
    {
        var productToSave = productViewToUpdate.ToProduct();
        await _repo.UpdateProductAsync(productToSave);
    }
}

