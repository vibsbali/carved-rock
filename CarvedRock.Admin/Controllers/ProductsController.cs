using CarvedRock.Admin.Logic;
using CarvedRock.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductLogic _productLogic;
    private readonly ILogger<ProductsController> _logger;

    //public List<ProductViewModel> Products {get;set;}
    public ProductsController(IProductLogic productLogic, ILogger<ProductsController> logger)
    {
        //Products = GetSampleProducts();
        _productLogic = productLogic;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productLogic.GetAllProducts();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productLogic.GetProductById(id);
        if (product == null)
        {
            _logger.LogInformation("Details not found for id {id}", id);
            return View("NotFound");
        }

        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }

    // POST: ProductsData/Create
    // To protect from over-posting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel productView)
    {
        if (ModelState.IsValid)
        {
            await _productLogic.AddNewProduct(productView);
            return RedirectToAction(nameof(Index));
        }

        return View(productView.ToProduct());
    }

    // GET: ProductsData/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation("No id passed for edit");
            return View("NotFound");
        }

        var productModel = await _productLogic.GetProductById(id.Value);
        if (productModel == null)
        {
            _logger.LogInformation("Edit details not found for id {id}", id);
            return View("NotFound");
        }

        return View(productModel);
    }

    // POST: ProductsData/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductViewModel productView)
    {
        if (id != productView.Id)
        {
            _logger.LogInformation("Id mismatch in passed information. " +
                                   "Id value {id} did not match model value of {productId}",
                id, productView.Id);
            return View("NotFound");
        }

        if (ModelState.IsValid)
        {
            await _productLogic.UpdateProduct(productView);
            return RedirectToAction(nameof(Index));
        }

        return View(productView);
    }

    // GET: ProductsData/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation("No id value was passed for deletion.");
            return View("NotFound");
        }

        var productModel = await _productLogic.GetProductById(id.Value);
        if (productModel == null)
        {
            _logger.LogInformation("Id to be deleted ({id}) does not exist in database.",
                id);
            return View("NotFound");
        }

        return View(productModel);
    }

    // POST: ProductsData/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productLogic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }
    // private List<ProductViewModel> GetSampleProducts()
    // {
    //     return new List<ProductViewModel> 
    //     {
    //         new ProductViewModel {Id = 1, Name = "Trailblazer", Price = 69.99M, IsActive = true,
    //             Description = "Great support in this high-top to take you to great heights and trails." },
    //         new ProductViewModel {Id = 2, Name = "Coastliner", Price = 49.99M, IsActive = true,
    //             Description = "Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats."},
    //         new ProductViewModel {Id = 3, Name = "Woodsman", Price = 64.99M, IsActive = true,
    //             Description = "All the insulation and support you need when wandering the rugged trails of the woods and backcountry." },
    //         new ProductViewModel {Id = 4, Name = "Basecamp", Price = 249.99M, IsActive = true,
    //             Description = "Great insulation and plenty of room for 2 in this spacious but highly-portable tent."},                            
    //     };
    // }
}