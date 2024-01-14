using BL.ServicePattern;
using Core.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Impl
{
    public class CustomerService : ServicePattern<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo) : base(customerRepo)
        {
            _customerRepo = customerRepo;
        }
    }
}
