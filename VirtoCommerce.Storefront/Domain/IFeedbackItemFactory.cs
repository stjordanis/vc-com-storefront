using VirtoCommerce.Storefront.Model.Feedback;

namespace VirtoCommerce.Storefront.Domain
{
    public interface IFeedbackItemFactory
    {
        void CreateItems();
        FeedbackItem GetItem(string name);
    }
}
