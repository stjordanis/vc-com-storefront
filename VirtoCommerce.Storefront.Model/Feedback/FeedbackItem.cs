using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.Storefront.Model.Feedback
{
    public class FeedbackItem
    {
        public FeedbackItem(string url)
        {
            Url = url;
            Parameters = new List<string>();
        }

        public string Url { get; }

        public string HttpMethod { get; set; }

        public bool AllowAdditionalParams { get; set; }

        public List<string> Parameters { get; set; }

        public async Task<(HttpStatusCode StatusCode, string Content)> SendRequestAsync(IEnumerable<string> parameters)
        {
            var requestParams = string.Join('&', AllowAdditionalParams ? Parameters.Concat(parameters) : Parameters);
            var client = new HttpClient();
            var bytes = Encoding.Default.GetBytes(requestParams);

            using (var stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                var requestMessage = new HttpRequestMessage()
                {
                    Method = new HttpMethod(HttpMethod ?? "GET"),
                    RequestUri = new Uri(Url + (Url.Contains('?') ? '&' : '?') + requestParams),
                    Content = new StreamContent(stream)
                };
                var responseMessage = await client.SendAsync(requestMessage);
                var content = await requestMessage.Content.ReadAsStringAsync();
                return (responseMessage.StatusCode, content);
            }
        }
    }
}
