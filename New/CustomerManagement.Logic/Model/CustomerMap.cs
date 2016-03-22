using FluentNHibernate.Mapping;

namespace CustomerManagement.Logic.Model
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            
            Map(x => x.Name).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.PrimaryEmail).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.SecondaryEmail).CustomType<string>().Access.CamelCaseField(Prefix.Underscore).Nullable();
            Map(x => x.Status).CustomType<CustomerStatus>();
            
            Component(x => x.EmailingSettings, y =>
            {
                y.References(x => x.Industry);
                y.Map(x => x.EmailingIsDisabled);
            });
        }
    }
}
