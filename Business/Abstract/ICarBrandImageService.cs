using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarBrandImageService
    {
        IDataResult<List<CarBrandImage>> GetAll();
        IDataResult<List<CarBrandImage>> GetBrandImagesByBrandId(int brandId);
        IDataResult<CarBrandImage> GetById(int brandImageId);
        IResult Add(IFormFile file, CarBrandImage brandImage);
        IResult AddCollective(IFormFile[] files, CarBrandImage brandImage);
        IResult Update(IFormFile file, CarBrandImage brandImage);
        IResult Delete(CarBrandImage brandImage);
        
    }
}
