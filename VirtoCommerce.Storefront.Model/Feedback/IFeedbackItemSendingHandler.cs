using System.Threading.Tasks;

namespace VirtoCommerce.Storefront.Model.Feedback
{
    public interface IFeedbackItemSending<T, U>
    {
        Task<U> SendAsync(T item);
    }
}
