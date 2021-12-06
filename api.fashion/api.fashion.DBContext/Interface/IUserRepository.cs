using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IUserRepository
    {
        BaseResponse Login(EntityLogin login);
        BaseResponse Insert(EntityUser user);
    }
}
