using CustomerManagement.Logic.Common;
using CustomerManagement.Logic.Model;
using Xunit;

namespace CustomerManagement.Tests.Fakes
{
    public class FakeEmailGateway : IEmailGateway
    {
        public int PromotionNotificationsSent { get; private set; }

        public Result SendPromotionNotification(string email, CustomerStatus newStatus)
        {
            PromotionNotificationsSent++;
            return Result.Ok();
        }

        public void ShouldContainNumberOfPromotionNotificationsSent(int number)
        {
            Assert.Equal(number, PromotionNotificationsSent);
        }
    }
}
