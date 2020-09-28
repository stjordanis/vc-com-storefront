using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VirtoCommerce.Storefront.Infrastructure;
using VirtoCommerce.Storefront.Model.Feedback;

namespace VirtoCommerce.Storefront.Controllers
{
    [StorefrontRoute]
    [ValidateAntiForgeryToken]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("call")]
        public async Task<IActionResult> CallService(Dictionary<string, string> data)
        {
            var name = Request.Headers["service"];
            if (!string.IsNullOrEmpty(name))
            {
                var parameters = data?.Select(p => $"{p.Key}={data[p.Key]}").ToList();
                var serviceResponse = await _feedbackService.GetItem(name).SendRequestAsync(parameters);
                var statusCode = (int)serviceResponse.StatusCode;
                if (statusCode == 200)
                {
                    if (TryParseJson(serviceResponse.Content, out var content))
                    {
                        return Json(content);
                    }
                    return Ok(serviceResponse.Content);
                }
                else
                {
                    return new StatusCodeResult(statusCode);
                }
            }
            return NotFound();
        }

        private bool TryParseJson(string json, out object result)
        {
            try
            {
                result = JsonConvert.DeserializeObject(json);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
