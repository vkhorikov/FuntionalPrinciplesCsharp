using System;
using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public class Customer : Entity
    {
        private readonly string _name;
        public virtual CustomerName Name => (CustomerName)_name;

        private readonly string _primaryEmail;
        public virtual Email PrimaryEmail => (Email)_primaryEmail;

        private string _secondaryEmail;
        public virtual Maybe<Email> SecondaryEmail
        {
            get { return _secondaryEmail == null ? null : (Email)_secondaryEmail; }
            protected set { _secondaryEmail = value.Unwrap(x => x.Value); }
        }

        public virtual EmailingSettings EmailingSettings { get; protected set; }
        public virtual CustomerStatus Status { get; protected set; }

        protected Customer()
        {
        }

        public Customer(CustomerName name, Email primaryEmail, Maybe<Email> secondaryEmail, Industry industry)
            : this()
        {
            _name = name;
            _primaryEmail = primaryEmail;
            SecondaryEmail = secondaryEmail;
            EmailingSettings = new EmailingSettings(industry, false);
            Status = CustomerStatus.Regular;
        }

        public virtual void DisableEmailing()
        {
            EmailingSettings = EmailingSettings.DisableEmailing();
        }

        public virtual void UpdateIndustry(Industry industry)
        {
            EmailingSettings = EmailingSettings.ChangeIndustry(industry);
        }

        public virtual bool CanBePromoted()
        {
            return Status != CustomerStatus.Gold;
        }

        public virtual void Promote()
        {
            if (!CanBePromoted())
                throw new InvalidOperationException();

            switch (Status)
            {
                case CustomerStatus.Regular:
                    Status = CustomerStatus.Preferred;
                    break;

                case CustomerStatus.Preferred:
                    Status = CustomerStatus.Gold;
                    break;

                case CustomerStatus.Gold:
                    throw new InvalidOperationException();

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
