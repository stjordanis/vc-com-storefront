using VirtoCommerce.Storefront.Model.Feedback;

namespace VirtoCommerce.Storefront.Domain
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackItemFactory _factory;

        public FeedbackService(IFeedbackItemFactory factory)
        {
            _factory = factory;
            Init();
        }

        private void Init()
        {
            _factory.CreateItem("TargetAccountV2");
            _factory.CreateItem("Location");
            _factory.CreateItem("GatedAssets");
        }

        public FeedbackItem GetItem(string name)
        {
            return _factory[name];
        }
    }
}
