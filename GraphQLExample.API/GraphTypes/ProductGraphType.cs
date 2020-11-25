using GraphQL.Types;

namespace GraphQLExample.API.GraphTypes
{
    using Models;

    public class ProductGraphType : ObjectGraphType<Product>
    {
        public ProductGraphType()
        {
            Field(x => x.ProductId, false);
            Field(x => x.Name, false);
            Field(x => x.Category, false);
            Field<ListGraphType<NonNullGraphType<StringGraphType>>>("photos", resolve: x => x.Source.Photos);
        }
    }
}
