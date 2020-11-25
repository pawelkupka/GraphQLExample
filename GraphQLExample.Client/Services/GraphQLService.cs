using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLExample.Client.Services
{
    using Constants;
    using Models;

    public class GraphQLService
    {
        private static readonly Lazy<GraphQLHttpClient> _clientHolder = new Lazy<GraphQLHttpClient>(CreateGraphQLClient);

        private static GraphQLHttpClient Client => _clientHolder.Value;

        public static async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = "query { products  { productId, name, category } }"
            };
            var response = await Client.SendQueryAsync<ProductsGraphQLResponse>(graphQLRequest);
            return response.Data.Products;
        }

        public static async Task<Product> GetProductAsync(int productId)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = $"query {{ product(productId: {productId})  {{ productId, name, category, photos }} }}"
            };
            var response = await Client.SendQueryAsync<ProductGraphQLResponse>(graphQLRequest);
            return response.Data.Product;
        }

        public static async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = $"query {{ productsByCategory(category: \"{category}\")  {{ productId, name, photos }} }}"
            };
            var response = await Client.SendQueryAsync<ProductsByCategoryGraphQLResponse>(graphQLRequest);
            return response.Data.ProductsByCategory;
        }

        private static GraphQLHttpClient CreateGraphQLClient()
        {
            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(BackendConstants.GraphQLApiUrl)
            };
            return new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
        }
    }
}
