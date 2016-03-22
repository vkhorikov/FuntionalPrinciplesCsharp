using CustomerManagement.Logic.Model;
using Xunit;

namespace CustomerManagement.Tests.Utils
{
    public static class CustomerExtensions
    {
        public static Customer WithName(this Customer customer, string name)
        {
            Assert.Equal(name, customer.Name);
            return customer;
        }

        public static Customer WithPrimaryEmail(this Customer customer, string primaryEmail)
        {
            Assert.Equal(primaryEmail, customer.PrimaryEmail);
            return customer;
        }

        public static Customer WithSecondaryEmail(this Customer customer, string secondaryEmail)
        {
            Assert.Equal(secondaryEmail, customer.SecondaryEmail.Value);
            return customer;
        }

        public static Customer WithNoSecondaryEmail(this Customer customer)
        {
            Assert.False(customer.SecondaryEmail.HasValue);
            return customer;
        }

        public static Customer WithIndustry(this Customer customer, string industry)
        {
            Assert.Equal(industry, customer.EmailingSettings.Industry.Name);
            return customer;
        }

        public static Customer WithEmailCampaign(this Customer customer, EmailCampaign emailCampaign)
        {
            Assert.Equal(emailCampaign, customer.EmailingSettings.EmailCampaign);
            return customer;
        }

        public static Customer WithStatus(this Customer customer, CustomerStatus status)
        {
            Assert.Equal(status, customer.Status);
            return customer;
        }
    }
}
