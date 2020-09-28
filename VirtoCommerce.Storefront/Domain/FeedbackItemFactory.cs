using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using VirtoCommerce.Storefront.Model.Feedback;

namespace VirtoCommerce.Storefront.Domain
{
    public class FeedbackItemFactory : IFeedbackItemFactory
    {
        private readonly IConfiguration _configuration;

        public FeedbackItemFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly Dictionary<string, FeedbackItem> _items = new Dictionary<string, FeedbackItem>();

        public FeedbackItem GetItem(string name) => _items[name];

        public void CreateItems()
        {
            var services = _configuration.GetSection("FeedbackServices").GetChildren();
            foreach (var service in services)
            {
                var url = service.GetSection("Url");
                if (url != null)
                {
                    bool.TryParse(service.GetSection("AllowAdditionalParams").Value, out var allowAdditionalParams);
                    var item = new FeedbackItem(url.Value)
                    {
                        HttpMethod = service.GetSection("Method").Value,
                        AllowAdditionalParams = allowAdditionalParams
                    };

                    var parameters = service.GetSection("Params");
                    if (parameters != null)
                    {
                        item.Parameters = parameters.GetChildren()
                            .ToList()
                            .Select(p => $"{p.GetValue<string>("Name")}={p.GetValue<string>("Value")}")
                            .ToList();
                    }
                    _items.Add(service.Key, item);
                }
                else
                {
                    throw new KeyNotFoundException("Url segment not found in config object with specified key.");
                }
            }
        }
    }
}
