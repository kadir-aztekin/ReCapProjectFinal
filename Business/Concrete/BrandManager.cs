using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using System.Linq;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        //[CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.Listed);

        }


        [CacheAspect]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId), Messages.Listed);
        }



        [ValidationAspect(typeof(BrandValidator), Priority = 1)]
        //[SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Added);

        }



        [ValidationAspect(typeof(BrandValidator), Priority = 1)]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<BrandDetailDto>> GetBrandDetails()
        {
            return new SuccessDataResult<List<BrandDetailDto>>(_brandDal.GetBrandDetails(), Messages.Listed);
        }

        public IDataResult<List<BrandDetailDto>> GetBrandDetailsByBrand(int brandId)
        {
            
            return new SuccessDataResult<List<BrandDetailDto>>(_brandDal.GetBrandDetails(c => c.Id == brandId));
        }
    }
}

