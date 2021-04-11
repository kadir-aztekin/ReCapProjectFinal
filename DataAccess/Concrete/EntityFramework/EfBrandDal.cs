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
    public class EfBrandDal : EfEntityRepositoryBase<Brand, CarInformationContext>, IBrandDal
    {
        public List<BrandDetailDto> GetBrandDetails(Expression<Func<Brand, bool>> filter = null)
        {
            using (CarInformationContext context = new CarInformationContext())
            {
                var result = from c in filter == null ? context.Brands : context.Brands.Where(filter)

                             join b in context.CarBrandImages
                                 on c.Id equals b.BrandId 


                             select new BrandDetailDto
                             {
                                 BrandId = c.Id,
                                 
                                 BrandName=c.BrandName,
                                 CarBrandImagePath = (from i in context.CarBrandImages where i.BrandId == b.BrandId select i.ImagePath).ToList()
                                 


                             };
                return result.ToList();
            }
        }
    }
}
