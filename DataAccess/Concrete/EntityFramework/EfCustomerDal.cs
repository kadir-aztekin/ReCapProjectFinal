using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarInformationContext>, ICustomerDal
    {


        public bool DeleteCustomerIfNotReturnDateNull(Customer customer)
        {
            using (CarInformationContext context = new CarInformationContext())
            {
                var find = context.Rentals.Any(i => i.CustomerId == customer.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(customer);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}