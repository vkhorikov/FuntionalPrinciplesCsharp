using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public class Industry : Entity
    {
        public const string CarsIndustry = "Cars";
        public const string PharmacyIndustry = "Pharmacy";

        public virtual string Name { get; set; }
    }
}
