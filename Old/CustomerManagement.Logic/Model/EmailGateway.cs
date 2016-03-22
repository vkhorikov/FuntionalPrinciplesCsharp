using System.Net.Mail;

namespace CustomerManagement.Logic.Model
{
    public class EmailGateway : IEmailGateway
    {
        public void SendPromotionNotification(string email, CustomerStatus newStatus)
        {
            SendEmail(email, "Congratulations!", "You've been promoted to " + newStatus);
        }

        private void SendEmail(string to, string subject, string body)
        {
            var message = new MailMessage("noreply@northwind.com", to, subject, body);
            var client = new SmtpClient();
            client.Send(message);
        }
    }
}
