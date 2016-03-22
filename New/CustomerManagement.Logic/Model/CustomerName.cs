using CustomerManagement.Logic.Common;

namespace CustomerManagement.Logic.Model
{
    public class CustomerName : ValueObject<CustomerName>
    {
        public string Value { get; }

        private CustomerName(string value)
        {
            Value = value;
        }

        public static Result<CustomerName> Create(Maybe<string> customerNameOrNothing)
        {
            return customerNameOrNothing
                .ToResult("Customer name should not be empty")
                .OnSuccess(name => name.Trim())
                .Ensure(name => name != string.Empty, "Customer name should not be empty")
                .Ensure(name => name.Length <= 200, "Customer name is too long")
                .Map(name => new CustomerName(name));
        }

        protected override bool EqualsCore(CustomerName other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator CustomerName(string customerName)
        {
            return Create(customerName).Value;
        }

        public static implicit operator string (CustomerName customerName)
        {
            return customerName.Value;
        }
    }
}
