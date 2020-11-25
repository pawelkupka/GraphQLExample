using System.Collections.Generic;

namespace GraphQLExample.API.Data
{
    using Models;

    public class ProductData
    {
        public static IEnumerable<Product> CreateProductList()
        {
            yield return new Product(1, "Product 1", "Category 1", new List<string>() { "photo1-1.jpg", "photo1-2.jpg" });
            yield return new Product(2, "Product 2", "Category 2", new List<string>() { "photo2-1.jpg", "photo2-2.jpg" });
            yield return new Product(3, "Product 3", "Category 1", new List<string>() { "photo3-1.jpg", "photo3-2.jpg" });
            yield return new Product(4, "Product 4", "Category 2", new List<string>() { "photo4-1.jpg", "photo4-2.jpg" });
            yield return new Product(5, "Product 5", "Category 2", new List<string>() { "photo5-1.jpg", "photo5-2.jpg" });
            yield return new Product(6, "Product 6", "Category 1", new List<string>() { "photo6-1.jpg", "photo6-2.jpg" });
            yield return new Product(7, "Product 7", "Category 1", new List<string>() { "photo7-1.jpg", "photo7-2.jpg" });
            yield return new Product(8, "Product 8", "Category 2", new List<string>() { "photo8-1.jpg", "photo8-2.jpg" });
            yield return new Product(9, "Product 9", "Category 2", new List<string>() { "photo9-1.jpg", "photo9-2.jpg" });
        }
    }
}
