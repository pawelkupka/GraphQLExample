using System.Threading.Tasks;
using GraphQL.Validation;

namespace GraphQLExample.API
{
    public class GraphQLInputValidationRule : IValidationRule
    {
        public Task<INodeVisitor> ValidateAsync(ValidationContext context) => Task.FromResult<INodeVisitor>(new EnterLeaveListener());
    }
}
