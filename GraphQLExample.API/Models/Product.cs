using System.Collections.Generic;

namespace GraphQLExample.API.Models
{
    public class Product
    {
        public Product(int productId, string name, string category, IReadOnlyList<string> photos)
        {
            ProductId = productId;
            Name = name;
            Category = category;
            Photos = photos;
        }

        public int ProductId { get; }
        public string Name { get; }
        public string Category { get; }
        public IReadOnlyList<string> Photos { get; }
    }
}
