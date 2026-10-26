using global::wleepay.Core.Repositories;
using global::wleepay.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Data.Repositories
{
  
        public class ProductRepository : IProductRepository
        {
            private readonly WPContext _context;

            public ProductRepository(WPContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<IEnumerable<Product>> GetProductsByOperatorAsync(string sochitelOperatorId)
            {
                if (string.IsNullOrWhiteSpace(sochitelOperatorId))
                    throw new ArgumentException("Sochitel Operator ID cannot be null or empty", nameof(sochitelOperatorId));

                return await _context.Products
                    .AsNoTracking()
                    .Where(p => !p.IsDeleted && p.SochitelOperatorId == sochitelOperatorId)
                    .OrderBy(p => p.Type)
                    .ThenBy(p => p.Price)
                    .ToListAsync();
            }

            /// <summary>
            /// Gets a product by its ID
            /// </summary>
            public async Task<Product> GetProductByIdAsync(string id)
            {
                return await _context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.StripeProductId == id && !p.IsDeleted);
            }

            /// <summary>
            /// Gets all products
            /// </summary>
            public async Task<IEnumerable<Product>> GetAllProductsAsync()
            {
                return await _context.Products
                    .AsNoTracking()
                    .Where(p => !p.IsDeleted)
                    .OrderBy(p => p.Type)
                    .ThenBy(p => p.Price)
                    .ToListAsync();
            }

            /// <summary>
            /// Creates a new product
            /// </summary>
            public async Task<Product> CreateProductAsync(Product product)
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }

            /// <summary>
            /// Updates an existing product
            /// </summary>
            public async Task<Product> UpdateProductAsync(Product product)
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));


                var existingProduct = await _context.Products.FindAsync(product.Id);
                if (existingProduct == null)
                    throw new InvalidOperationException($"Product with ID {product.Id} not found");

                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
                return product;
            }

            /// <summary>
            /// Deletes a product (soft delete)
            /// </summary>
            public async Task<bool> DeleteProductAsync(Guid id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return false;

                product.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }

            /// <summary>
            /// Checks if a product exists
            /// </summary>
            public async Task<bool> ProductExistsAsync(Guid id)
            {
                return await _context.Products
                    .AnyAsync(p => p.Id == id && !p.IsDeleted);
            }


        /// <summary>
        /// Gets all operators ordered by country and name
        /// </summary>
        public async Task<IEnumerable<Operator>> GetAllOperatorsAsync()
        {
            return await _context.Operators
                .AsNoTracking()
                .Where(o => !o.IsDeleted && o.SochitelId != "2482")
                .OrderBy(o => o.CountryName)
                .ThenBy(o => o.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Gets an operator by its ID
        /// </summary>
        public async Task<Operator> GetOperatorByIdAsync(Guid id)
        {
            return await _context.Operators
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
        }

        /// <summary>
        /// Gets an operator by its Sochitel ID
        /// </summary>
        public async Task<Operator> GetOperatorBySochitelIdAsync(string sochitelId)
        {
            if (string.IsNullOrWhiteSpace(sochitelId))
                throw new ArgumentException("Sochitel ID cannot be null or empty", nameof(sochitelId));

            return await _context.Operators
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.SochitelId == sochitelId && !o.IsDeleted);
        }
       
        /// <summary>
        /// Checks if an operator exists
        /// </summary>
        public async Task<bool> OperatorExistsAsync(Guid id)
        {

            return await _context.Operators
                .AnyAsync(o => o.Id == id && !o.IsDeleted);
        }
    }


    
}
