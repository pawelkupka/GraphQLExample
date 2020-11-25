using System.Collections.Generic;

namespace GraphQLExample.Client.Models
{
    public class ProductsGraphQLResponse
    {
        public ProductsGraphQLResponse(IEnumerable<Product> products)
        {
            Products = products;
        }

        public IEnumerable<Product> Products { get; }
    }
}
