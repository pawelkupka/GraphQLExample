using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;

namespace GraphQLExample.API.Queries
{
    using Data;
    using GraphTypes;
    using Models;

    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery()
        {
            Name = "ProductQuery";
            Field<ListGraphType<ProductGraphType>>("products", "Query for all products", null, context => ProductData.CreateProductList());
            Field<ProductGraphType>("product", "Query a specific product",
                    new QueryArguments(
                        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "productId", Description = "Product ID" }
                        ),
                    context => GetProductById(context.GetArgument<int>("productId")));
            
            
            Field<ListGraphType<ProductGraphType>>("productsByCategory", "Query products by category",
                    new QueryArguments(
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "category", Description = "Product Category" }
                        ),
                    context => GetProductsByCategory(context.GetArgument<string>("category")));
        }

        Product GetProductById(int productId)
        {
            return ProductData.CreateProductList().Single(x => x.ProductId == productId);
        }

        IEnumerable<Product> GetProductsByCategory(string category)
        {
            return ProductData.CreateProductList().Where(x => x.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
