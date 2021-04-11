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
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _imageDal;

        public CarImageManager(ICarImageDal imageDal)
        {
            _imageDal = imageDal;
        }



        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_imageDal.GetAll(), Messages.CarsListed);
        }



        [CacheAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            

            var result = _imageDal.GetAll(i => i.CarId == carId);

            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarImage>>(result);
            }

            List<CarImage> images = new List<CarImage>();
            images.Add(new CarImage() { CarId = 0, Id = 0, Date = DateTime.Now, ImagePath = "/images/car-rent.png" });

            return new SuccessDataResult<List<CarImage>>(images);
        }



        [CacheAspect]
        public IDataResult<CarImage> GetById(int imageId)
        {
            return new SuccessDataResult<CarImage>(_imageDal.Get(i => i.Id == imageId));

        }



        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(
                
                CheckIfImageExtensionValid(file));

            if (result != null)
            {
                return result;
            }
                      

            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _imageDal.Add(carImage);
            return new SuccessResult(Messages.CarsImageAdded);

        }

        public IResult AddCollective(IFormFile[] files, CarImage carImage)
        {
            List<CarImage> gelAll = _imageDal.GetAll(i => i.CarId == carImage.CarId);
            var result = (gelAll.Count() >= 6);

            if (!result)
            {
                foreach (var file in files)
                {
                    carImage = new CarImage { CarId = carImage.CarId };
                    Add(file, carImage);
                }
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageLimitExceeded);
        }



        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(
               CheckIfImageExtensionValid(file));

            if (result != null)
            {
                return result;
            }

            CarImage oldCarImage = GetById(carImage.Id).Data;
            carImage.ImagePath = FileHelper.Update(file, oldCarImage.ImagePath);
            carImage.Date = DateTime.Now;
            carImage.CarId = oldCarImage.CarId;
            _imageDal.Update(carImage);
            return new SuccessResult(Messages.CarsImageUpdated);
        }



        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(CarImage carImage)
        {

            string oldPath = GetById(carImage.Id).Data.ImagePath;
            FileHelper.Delete(oldPath);
            _imageDal.Delete(carImage);
            return new SuccessResult(Messages.CarsImageDeleted);
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
