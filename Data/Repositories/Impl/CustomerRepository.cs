using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Impl
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository, IRepository<Customer>    
    {
        public CustomerRepository(MyDataBaseContext context) : base (context) { }      

        public Task<Customer> GetByName(string name)
        {
            return context.Set<Customer>().FirstOrDefaultAsync(customer => customer.Name == name);
        }
    }
}
