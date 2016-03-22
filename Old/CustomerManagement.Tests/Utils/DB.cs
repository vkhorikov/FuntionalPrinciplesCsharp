using System;
using System.Collections.Generic;
using CustomerManagement.Logic.Common;
using CustomerManagement.Logic.Model;
using CustomerManagement.Logic.Utils;
using Xunit;

namespace CustomerManagement.Tests.Utils
{
    public class DB : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public DB()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Customer ShouldContainCustomer(string name)
        {
            var repository = new CustomerRepository(_unitOfWork);
            Customer customer = repository.GetByName(name);

            Assert.NotNull(customer);

            return customer;
        }

        public Customer ShouldContainCustomer(long id)
        {
            var repository = new CustomerRepository(_unitOfWork);
            Customer customer = repository.GetById(id);

            Assert.NotNull(customer);

            return customer;
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
