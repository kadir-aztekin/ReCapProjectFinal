using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
       //IDataResult< List<OperationClaim>> GetClaims(User user);
       // void Add(User user);
       // User GetByMail(string email);

        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        IDataResult<User> GetByEmail(string email);
    }
}
