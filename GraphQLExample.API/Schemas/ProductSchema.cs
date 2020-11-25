using GraphQL.Types;

namespace GraphQLExample.API.Schemas
{
    using Queries;

    public class ProductSchema : Schema
    {
        public ProductSchema() => Query = new ProductQuery();
    }
}
