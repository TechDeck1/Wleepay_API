using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wleepay.Core.Models;


namespace wleepay.Core.Repositories
{
        public interface IProductRepository
        {
            /// <summary>
            /// Gets all products by operator ID
            /// </summary>
            Task<IEnumerable<Product>> GetProductsByOperatorAsync(string sochitelOperatorId);

            /// <summary>
            /// Gets a product by its ID
            /// </summary>
            Task<Product> GetProductByIdAsync(string id);

            /// <summary>
            /// Gets all products
            /// </summary>
            Task<IEnumerable<Product>> GetAllProductsAsync();

            Task<bool> ProductExistsAsync(Guid id);


        /// <summary>
        /// Gets all operators ordered by country and name
        /// </summary>
        Task<IEnumerable<Operator>> GetAllOperatorsAsync();

        /// <summary>
        /// Gets an operator by its ID
        /// </summary>
        Task<Operator> GetOperatorByIdAsync(Guid id);

        /// <summary>
        /// Gets an operator by its Sochitel ID
        /// </summary>
        Task<Operator> GetOperatorBySochitelIdAsync(string sochitelId);
       
        Task<bool> OperatorExistsAsync(Guid id);
    }
}
