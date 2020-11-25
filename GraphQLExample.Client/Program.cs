using System.Threading.Tasks;

namespace GraphQLExample.Client
{
    using Services;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var products = await GraphQLService.GetProductsAsync();
            var product = await GraphQLService.GetProductAsync(2);
            var productsByCategory = await GraphQLService.GetProductsByCategoryAsync("Category 2");
        }
    }
}
