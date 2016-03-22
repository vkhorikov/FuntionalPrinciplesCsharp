using FluentNHibernate.Mapping;

namespace CustomerManagement.Logic.Model
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.PrimaryEmail);
            Map(x => x.SecondaryEmail).Nullable();
            Map(x => x.EmailCampaign).CustomType<EmailCampaign>();
            Map(x => x.Status).CustomType<CustomerStatus>();

            References(x => x.Industry);
        }
    }
}
