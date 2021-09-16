using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace VirtoCommerce.Storefront.Middleware
{
    public class UrlReplaceMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlReplaceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var regex = new Regex("/{2,}");
            var url = context.Request.Path.Value;
            var isNeedToRedirect = false;
            if (regex.IsMatch(url))
            {
                url = regex.Replace(url, "/");
                isNeedToRedirect = true;
            }

            if (url.Any(c => char.IsUpper(c)))
            {
                url = url.ToLower();
                isNeedToRedirect = true;
            }

            if (isNeedToRedirect)
            {
                context.Request.Path = url;
                context.Response.Redirect(url);
            }
            await _next.Invoke(context);
        }
    }
}
