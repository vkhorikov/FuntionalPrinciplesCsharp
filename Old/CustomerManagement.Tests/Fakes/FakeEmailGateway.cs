using CustomerManagement.Logic.Model;
using Xunit;

namespace CustomerManagement.Tests.Fakes
{
    public class FakeEmailGateway : IEmailGateway
    {
        public int PromotionNotificationsSent { get; private set; }

        public void SendPromotionNotification(string email, CustomerStatus newStatus)
        {
            PromotionNotificationsSent++;
        }

        public void ShouldContainNumberOfPromotionNotificationsSent(int number)
        {
            Assert.Equal(number, PromotionNotificationsSent);
        }
    }
}
