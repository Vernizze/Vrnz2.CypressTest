using System;

namespace Vrnz2.CypressTest.Api.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal UnitValue { get; set; }
    }
}
