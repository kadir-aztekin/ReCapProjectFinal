using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Uploads.FileHelper;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarBrandImageManager : ICarBrandImageService
    {
        private ICarBrandImageDal _imageDal;

        public CarBrandImageManager(ICarBrandImageDal brandImageDal)
        {
            _imageDal = brandImageDal;
        }



        [CacheAspect]
        public IDataResult<List<CarBrandImage>> GetAll()
        {
            return new SuccessDataResult<List<CarBrandImage>>(_imageDal.GetAll(), Messages.BrandListed);
        }



        [CacheAspect]
        public IDataResult<List<CarBrandImage>> GetBrandImagesByBrandId(int brandId)
        {


            var result = _imageDal.GetAll(i => i.BrandId == brandId);

            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarBrandImage>>(result);
            }

            List<CarBrandImage> brandImages = new List<CarBrandImage>();
            brandImages.Add(new CarBrandImage() { BrandId = 0, Id = 0, Date = DateTime.Now, ImagePath = "/images/carBrand-rent.png" });

            return new SuccessDataResult<List<CarBrandImage>>(brandImages);
        }



        [CacheAspect]
        public IDataResult<CarBrandImage> GetById(int brandImageId)
        {
            return new SuccessDataResult<CarBrandImage>(_imageDal.Get(i => i.Id == brandImageId));

        }



        [CacheRemoveAspect("ICarBrandImageService.Get")]
        [SecuredOperation("admin")]
        public IResult Add(IFormFile file, CarBrandImage brandImage)
        {

            var result = BusinessRules.Run(
                CheckIfImageExtensionValid(file));

            if (result != null)
            {
                return result;
            }


            brandImage.ImagePath = BrandFileHelper.AddBrand(file);
            brandImage.Date = DateTime.Now;
            _imageDal.Add(brandImage);
            return new SuccessResult(Messages.BrandImageAdded);

        }


        public IResult AddCollective(IFormFile[] files, CarBrandImage carBrandImage)
        {
            List<CarBrandImage> gelAll = _imageDal.GetAll(i => i.BrandId == carBrandImage.BrandId);
            var result = (gelAll.Count() >= 1);

            if (!result)
            {
                foreach (var file in files)
                {
                    carBrandImage = new CarBrandImage { BrandId = carBrandImage.BrandId };
                    Add(file, carBrandImage);
                }
                return new SuccessResult();

            }

            return new ErrorResult(Messages.logoLimitExceeded);


        }



        [CacheRemoveAspect("ICarBrandImageService.Get")]
        [SecuredOperation("admin")]
        public IResult Update(IFormFile file, CarBrandImage brandImage)
        {
            var result = BusinessRules.Run(
               CheckIfImageExtensionValid(file));

            if (result != null)
            {
                return result;
            }

            CarBrandImage oldCarBrandImage = GetById(brandImage.Id).Data;
            brandImage.ImagePath = FileHelper.Update(file, oldCarBrandImage.ImagePath);
            brandImage.Date = DateTime.Now;
            brandImage.BrandId = oldCarBrandImage.BrandId;
            _imageDal.Update(brandImage);
            return new SuccessResult(Messages.BrandImageUpdated);
        }



        [CacheRemoveAspect("ICarBrandImageService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(CarBrandImage brandImage)
        {

            string oldPath = GetById(brandImage.Id).Data.ImagePath;
            BrandFileHelper.Delete(oldPath);
            _imageDal.Delete(brandImage);
            return new SuccessResult(Messages.Deleted);
        }

        

        private IResult CheckIfImageExtensionValid(IFormFile file)
        {
            string[] validImageFileTypes = { ".JPG", ".JPEG", ".PNG", ".TIF", ".TIFF", ".GIF", ".BMP", ".ICO", ".WEBP" };
            var result = validImageFileTypes.Any(t => t == Path.GetExtension(file.FileName).ToUpper());
            if (!result)
            {
                return new ErrorResult(Messages.InvalidExtension);
            }
            return new SuccessResult();
        }


    }
}
//public IResult CheckIfImageCount(CarBrandImage carBrandImage)
//{
//    List<CarBrandImage> gelAll = _imageDal.GetAll(i => i.BrandId == carBrandImage.BrandId);
//    var result = (gelAll.Count() >= 1);

//    if (result)
//    {

//        return new ErrorResult(Messages.logoLimitExceeded);
//    }
//    return new SuccessResult();
//}