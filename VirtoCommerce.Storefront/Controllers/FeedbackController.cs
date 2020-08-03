using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("targetAccountV2")]
        public IActionResult TargetAccountV2(string companyName, string email)
        {
            Task.Run(() => _feedbackService.GetItem("TargetAccountV2").SendRequest($"CompanyName={companyName}", $"Email={email}"));
            return Ok();
        }


        [HttpPost("location")]
        public IActionResult Location(string ip, string email)
        {
            Task.Run(() => _feedbackService.GetItem("Location").SendRequest($"ip={ip}", $"Email={email}"));
            return Ok();
        }

        [HttpPost("gatedAssets")]
        public IActionResult GatedAssets(string assetId, string email)
        {
            Task.Run(() => _feedbackService.GetItem("GatedAssets").SendRequest($"assetId={assetId}", $"Email={email}"));
            return Ok();
        }
    }
}
