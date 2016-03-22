using System;
using System.Net.Mail;
using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public class EmailGateway : IEmailGateway
    {
        public Result SendPromotionNotification(string email, CustomerStatus newStatus)
        {
            return SendEmail(email, "Congratulations!", "You've been promoted to " + newStatus);
        }

        private Result SendEmail(string to, string subject, string body)
        {
            var message = new MailMessage("noreply@northwind.com", to, subject, body);
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Send(message);
                    return Result.Ok();
                }
                catch (SmtpException)
                {
                    return Result.Fail("Unable to send the email");
                }
            }
        }
    }
}
