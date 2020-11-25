using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GraphQLExample.API
{
    public class GraphQLSettings
    {
        public GraphQLSettings(bool enableMetrics, Func<HttpContext, IDictionary<string, object>> buildUserContext) 
            : this(enableMetrics, buildUserContext, "/")
        {
        }

        public GraphQLSettings(bool enableMetrics, Func<HttpContext, IDictionary<string, object>> buildUserContext, PathString path)
        {
            Path = path;
            EnableMetrics = enableMetrics;
            BuildUserContext = buildUserContext;
        }
            
        public PathString Path { get; }
        public bool EnableMetrics { get; }
        public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; }
    }
}
