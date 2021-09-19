using System;
using System.Collections.Generic;
using System.Linq;
using Vrnz2.CypressTest.Api.Data.Entities;

namespace Vrnz2.CypressTest.Api.Data.Repositories
{
    public class OrdersRepository
    {
        #region Variables

        private static OrdersRepository _instance;

        public Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        #endregion

        #region Constructors

        private OrdersRepository() { }

        #endregion

        #region Attributes

        public static OrdersRepository Instance
        {
            get
            {
                _instance = _instance ?? new OrdersRepository();


                return _instance;
            }
        }

        #endregion

        #region Methods

        public void AddOrder(Order order)
            => _orders.Add(order.Id, order);

        public Order GetOrder(Guid orderId)
        {
            _orders.TryGetValue(orderId, out Order result);

            return result;
        }

        public List<Order> GetOrders()
            => _orders.Values.ToList();

        public int GetOrdersQtt()
            => _orders.Count;

        public void UpdateOrder(Order order)
        {
            if (_orders.TryGetValue(order.Id, out Order result))
            {
                result.Quantity = order.Quantity;
                result.Product = order.Product;
                result.Customer = order.Customer;

                _orders[order.Id] = result;
            }
        }

        public void DeleteOrder(Guid orderId)
        {
            if (_orders.TryGetValue(orderId, out Order result))
                _orders.Remove(orderId);
        }

        #endregion
    }
}
