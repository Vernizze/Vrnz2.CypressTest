using System;
using System.Collections.Generic;
using System.Linq;
using Vrnz2.CypressTest.Api.Data.Entities;

namespace Vrnz2.CypressTest.Api.Data.Repositories
{
    public class ProductsRepository
    {
        #region Variables

        private static ProductsRepository _instance;

        public Dictionary<Guid, Product> _products = new Dictionary<Guid, Product>();

        #endregion

        #region Constructors

        private ProductsRepository() { }

        #endregion

        #region Attributes

        public static ProductsRepository Instance 
        {
            get 
            {
                _instance = _instance ?? new ProductsRepository();


                return _instance;
            }
        }

        #endregion

        #region Methods

        public void AddProduct(Product product)
            => _products.Add(product.Id, product);

        public Product GetProduct(Guid productId)
        {
            _products.TryGetValue(productId, out Product result);

            return result;
        }

        public List<Product> GetProducts() 
            => _products.Values.ToList();

        public void UpdateProduct(Product product)
        {
            if (_products.TryGetValue(product.Id, out Product result)) 
            {
                result.Description = product.Description;
                result.UnitValue = product.UnitValue;

                _products[product.Id] = result;
            }
        }

        public void DeleteProduct(Guid productId)
        {
            if (_products.TryGetValue(productId, out Product result))
                _products.Remove(productId);
        }

        #endregion
    }
}
