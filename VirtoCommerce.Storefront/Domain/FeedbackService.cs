using VirtoCommerce.Storefront.Model.Feedback;

namespace VirtoCommerce.Storefront.Domain
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackItemFactory _factory;

        public FeedbackService(IFeedbackItemFactory factory)
        {
            _factory = factory;
            _factory.CreateItems();
        }

        public FeedbackItem GetItem(string name)
        {
            return _factory.GetItem(name);
        }
    }
}
