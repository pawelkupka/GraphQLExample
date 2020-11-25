using System.Collections.Generic;

namespace GraphQLExample.Client.Models
{
    public class ProductsByCategoryGraphQLResponse
    {
        public ProductsByCategoryGraphQLResponse(IEnumerable<Product> productsByCategory)
        {
            ProductsByCategory = productsByCategory;
        }

        public IEnumerable<Product> ProductsByCategory { get; }
    }
}
