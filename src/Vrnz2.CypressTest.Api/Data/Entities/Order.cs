using System;

namespace Vrnz2.CypressTest.Api.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
