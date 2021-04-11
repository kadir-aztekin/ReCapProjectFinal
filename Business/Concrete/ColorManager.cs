using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Entities.DTOs;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.Listed);
        }

        
        
        
        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == colorId), Messages.Listed);
        }

        
        
        [ValidationAspect(typeof(ColorValidator), Priority =1)]
        [CacheRemoveAspect("IColorService.Get")]
        //[SecuredOperation("admin")]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.Added);
        }

        
        
        
        [ValidationAspect(typeof(ColorValidator), Priority =1)]
        [CacheRemoveAspect("IColorService.Get")]
        [SecuredOperation("admin")]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.Deleted);
        }
    }
}