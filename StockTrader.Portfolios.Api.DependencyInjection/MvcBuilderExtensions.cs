using Microsoft.AspNetCore.Mvc.ApplicationParts;
using StockTrader.Portfolios.Api;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddPortfoliosControllers(this IMvcBuilder builder)
        {
            var assembly = Library.Assembly;
            builder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
            return builder;
        }
    }
}
