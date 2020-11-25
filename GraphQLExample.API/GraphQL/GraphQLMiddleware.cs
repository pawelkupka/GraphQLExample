using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GraphQL;
using GraphQL.Common.Request;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation;
using Newtonsoft.Json;

namespace GraphQLExample.API
{
    public class GraphQLMiddleware
    {
        readonly static JsonSerializer _serializer = new JsonSerializer();
        readonly RequestDelegate _next;
        readonly GraphQLSettings _settings;
        readonly IDocumentExecuter _executer;
        readonly IDocumentWriter _writer;

        public GraphQLMiddleware(
            RequestDelegate next, 
            GraphQLSettings settings, 
            IDocumentExecuter executer, 
            IDocumentWriter writer)
        {
            _next = next;
            _settings = settings;
            _executer = executer;
            _writer = writer;
        }

        public Task Invoke(HttpContext context, ISchema schema)
        {
            var (isGraphQLRequest, request) = IsGraphQLRequest(context);
            if (isGraphQLRequest && request is GraphQLRequest)
            {
                return ExecuteAsync(context, schema, request);
            }
            return _next(context);
        }

        [return: System.Diagnostics.CodeAnalysis.MaybeNull]
        static T Deserialize<T>(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            return _serializer.Deserialize<T>(jsonTextReader);
        }

        (bool isGraphQLRequest, GraphQLRequest request) IsGraphQLRequest(HttpContext context)
        {
            try
            {
                return (context.Request.Path.StartsWithSegments(_settings.Path) && context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase),
                            Deserialize<GraphQLRequest?>(context.Request.Body));
            }
            catch
            {
                return (false, null);
            }
        }

        async Task ExecuteAsync(HttpContext context, ISchema schema, GraphQLRequest request)
        {
            var result = await _executer.ExecuteAsync(options =>
            {
                options.Schema = schema;
                options.Query = request.Query;
                options.OperationName = request.OperationName ?? string.Empty;
                options.Inputs = request.Variables?.ToInputs();
                options.UserContext = _settings.BuildUserContext.Invoke(context);
                options.ValidationRules = DocumentValidator.CoreRules.Concat(new[] { new GraphQLInputValidationRule() });
                options.EnableMetrics = _settings.EnableMetrics;
                if (_settings.EnableMetrics)
                {
                    options.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                }
            });
            await WriteResponseAsync(context, result).ConfigureAwait(false);
        }

        Task WriteResponseAsync(in HttpContext context, in ExecutionResult result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Errors?.Any() is true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
            return _writer.WriteAsync(context.Response.Body, result);
        }
    }
}
