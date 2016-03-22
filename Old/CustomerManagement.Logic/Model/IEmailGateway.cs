using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public interface IEmailGateway
    {
        void SendPromotionNotification(string email, CustomerStatus newStatus);
    }
}
