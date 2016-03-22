using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public class Customer : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual string PrimaryEmail { get; protected set; }
        public virtual string SecondaryEmail { get; protected set; }
        public virtual Industry Industry { get; protected set; }
        public virtual EmailCampaign EmailCampaign { get; protected set; }
        public virtual CustomerStatus Status { get; protected set; }

        protected Customer()
        {
        }

        public Customer(string name, string primaryEmail, string secondaryEmail, Industry industry)
            : this()
        {
            Name = name;
            PrimaryEmail = primaryEmail;
            SecondaryEmail = secondaryEmail;
            Industry = industry;
            EmailCampaign = GetEmailCampaign(industry);
            Status = CustomerStatus.Regular;
        }

        private EmailCampaign GetEmailCampaign(Industry industry)
        {
            if (industry.Name == Industry.CarsIndustry)
                return EmailCampaign.LatestCarModels;

            if (industry.Name == Industry.PharmacyIndustry)
                return EmailCampaign.PharmacyNews;

            return EmailCampaign.Generic;
        }

        public virtual void DisableEmailing()
        {
            EmailCampaign = EmailCampaign.None;
        }

        public virtual void UpdateIndustry(Industry industry)
        {
            if (EmailCampaign == EmailCampaign.None)
                return;

            EmailCampaign = GetEmailCampaign(industry);
            Industry = industry;
        }

        public virtual bool CanBePromoted()
        {
            return Status != CustomerStatus.Gold;
        }

        public virtual void Promote()
        {
            if (Status == CustomerStatus.Regular)
            {
                Status = CustomerStatus.Preferred;
            }
            else
            {
                Status = CustomerStatus.Gold;
            }
        }
    }
}
