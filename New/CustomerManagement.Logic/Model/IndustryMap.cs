using FluentNHibernate.Mapping;

namespace CustomerManagement.Logic.Model
{
    public class IndustryMap : ClassMap<Industry>
    {
        public IndustryMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
        }
    }
}
