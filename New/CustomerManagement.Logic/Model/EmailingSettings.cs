using System;
using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public class EmailingSettings : ValueObject<EmailingSettings>
    {
        public Industry Industry { get; }
        public bool EmailingIsDisabled { get; }
        public EmailCampaign EmailCampaign => GetEmailCampaign(Industry, EmailingIsDisabled);

        private EmailingSettings()
        {
        }

        public EmailingSettings(Industry industry, bool emailingIsDisabled)
            : this()
        {
            Industry = industry;
            EmailingIsDisabled = emailingIsDisabled;
        }

        public EmailingSettings DisableEmailing()
        {
            return new EmailingSettings(Industry, true);
        }

        public EmailingSettings ChangeIndustry(Industry industry)
        {
            return new EmailingSettings(industry, EmailingIsDisabled);
        }

        private EmailCampaign GetEmailCampaign(Industry industry, bool emailingIsDisabled)
        {
            if (emailingIsDisabled)
                return EmailCampaign.None;

            if (industry == Industry.Cars)
                return EmailCampaign.LatestCarModels;

            if (industry == Industry.Pharmacy)
                return EmailCampaign.PharmacyNews;

            if (industry == Industry.Other)
                return EmailCampaign.Generic;

            throw new ArgumentException();
        }

        protected override bool EqualsCore(EmailingSettings other)
        {
            return Industry == other.Industry && EmailingIsDisabled == other.EmailingIsDisabled;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Industry.GetHashCode();
                hashCode = (hashCode * 397) ^ EmailingIsDisabled.GetHashCode();
                return hashCode;
            }
        }
    }
}
