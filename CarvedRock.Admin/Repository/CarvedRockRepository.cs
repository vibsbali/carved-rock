﻿using CarvedRock.Admin.Data;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Admin.Repository;

public class CarvedRockRepository : ICarvedRockRepository
{
    private readonly ProductContext _context;

    public CarvedRockRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == productId);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product; // will have updated ID value
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (_context.Products.Any(e => e.Id == product.Id))
            {
                // product exists and update exception is real
                throw;
            }
            // caught and swallowed exception can occur if 
            // the other update was a delete
        }
    }

    public async Task RemoveProductAsync(int productIdToRemove)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productIdToRemove);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}