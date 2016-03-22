using System.Linq;
using CustomerManagement.Logic.Common;
using CustomerManagement.Logic.Utils;

namespace CustomerManagement.Logic.Model
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Customer GetByName(string name)
        {
            return _unitOfWork.Query<Customer>()
                .SingleOrDefault(x => x.Name == name);
        }
    }
}
