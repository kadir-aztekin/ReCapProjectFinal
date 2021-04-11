using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarInformationContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (CarInformationContext context = new CarInformationContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             
                             join b in context.Brands
                                 on c.BrandId equals b.Id
                             join co in context.Colors
                                 on c.ColorId equals co.Id
                            
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandId = b.Id,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                

                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ImagePath = (from i in context.CarImages where i.CarId == c.Id select i.ImagePath).ToList(),
                                 CarBrandImagePath = (from i in context.CarBrandImages where i.BrandId == b.Id select i.ImagePath).ToList(),
                                

                             };
                return result.ToList();
            }
        }



        public bool DeleteCarIfNotReturnDateNull(Car car)
        {
            using (CarInformationContext context = new CarInformationContext())
            {
                var find = context.Rentals.Any(i => i.CarId == car.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(car);
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }


    }
}