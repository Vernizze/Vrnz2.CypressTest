using System;
using Vrnz2.Infra.CrossCutting.Types;

namespace Vrnz2.CypressTest.Api.Data.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Cpf Cpf { get; set; }
    }
}
