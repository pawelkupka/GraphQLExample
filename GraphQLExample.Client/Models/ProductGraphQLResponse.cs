namespace GraphQLExample.Client.Models
{
    public class ProductGraphQLResponse
    {
        public ProductGraphQLResponse(Product product)
        {
            Product = product;
        }

        public Product Product { get; }
    }
}
