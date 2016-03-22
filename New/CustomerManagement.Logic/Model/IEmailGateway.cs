using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public interface IEmailGateway
    {
        Result SendPromotionNotification(string email, CustomerStatus newStatus);
    }
}
