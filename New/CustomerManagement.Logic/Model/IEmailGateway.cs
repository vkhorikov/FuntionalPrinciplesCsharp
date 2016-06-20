using CSharpFunctionalExtensions;


namespace CustomerManagement.Logic.Model
{
    public interface IEmailGateway
    {
        Result SendPromotionNotification(string email, CustomerStatus newStatus);
    }
}
