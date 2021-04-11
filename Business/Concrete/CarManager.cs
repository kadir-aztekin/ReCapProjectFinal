using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Filter;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);
        }
        
        

        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=> c.Id == carId), Messages.Listed);
        }

        
        
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=> c.ColorId == colorId), Messages.Listed);
        }

        
        
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return  new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.Listed);
        }

        
        
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator), Priority =1)]
        public IResult  Add(Car car)
        {
            if (car.DailyPrice < 0)
            {
                return new ErrorResult(Messages.InvalidEntry);
            }
            else
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.Added);
            }
        }

        
        
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(Car car)
        {
            var result = _carDal.DeleteCarIfNotReturnDateNull(car);
            if (result)
            {
                return new SuccessResult(Messages.Deleted);
            }

            return new ErrorResult(Messages.NotDeleted);
        }

        
        
        
        [ValidationAspect(typeof(CarValidator), Priority =1)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        
        
        
        [TransactionScopeAspect]
        public IResult TransactionalTest(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId && c.BrandId==brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCar(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c=> c.Id == id));
        }

        
    }
}
