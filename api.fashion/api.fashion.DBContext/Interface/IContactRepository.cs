using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IContactRepository
    {   BaseResponse Insert(EntityContact contact);
    
    }
}
