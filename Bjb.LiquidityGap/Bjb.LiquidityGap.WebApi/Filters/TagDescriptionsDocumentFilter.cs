using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace Bjb.LiquidityGap.WebApi.Filters
{
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var item in context.ApiDescriptions)
            {
                var name = item.ActionDescriptor;
                MethodInfo methodInfo;
                var ss = item.TryGetMethodInfo(out methodInfo);
                var controllerFilters = methodInfo.DeclaringType.CustomAttributes;
                var actionFilters = methodInfo.DeclaringType.CustomAttributes;
                swaggerDoc.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag { Name ="l", Description = "Browse/manage the product catalog" },
                };
            }

        }
    }
}
