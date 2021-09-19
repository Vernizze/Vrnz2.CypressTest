using System;
using System.Collections.Generic;
using System.Linq;
using Vrnz2.CypressTest.Api.Data.Entities;

namespace Vrnz2.CypressTest.Api.Data.Repositories
{
    public class CustomersRepository
    {
        #region Variables

        private static CustomersRepository _instance;

        public Dictionary<Guid, Customer> _customers = new Dictionary<Guid, Customer>();

        #endregion

        #region Constructors

        private CustomersRepository() { }

        #endregion

        #region Attributes

        public static CustomersRepository Instance
        {
            get
            {
                _instance = _instance ?? new CustomersRepository();


                return _instance;
            }
        }

        #endregion

        #region Methods

        public void AddCustomer(Customer customer)
            => _customers.Add(customer.Id, customer);

        public Customer GetProduct(Guid customerId)
        {
            _customers.TryGetValue(customerId, out Customer result);

            return result;
        }

        public List<Customer> GetCustomers()
            => _customers.Values.ToList();

        public void UpdateCustomer(Customer customer)
        {
            if (_customers.TryGetValue(customer.Id, out Customer result))
            {
                result.Name = customer.Name;
                result.Cpf = customer.Cpf;

                _customers[customer.Id] = result;
            }
        }

        public void DeleteCustomer(Guid customerId)
        {
            if (_customers.TryGetValue(customerId, out Customer result))
                _customers.Remove(customerId);
        }

        #endregion
    }
}
