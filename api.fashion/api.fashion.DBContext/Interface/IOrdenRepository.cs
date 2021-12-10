using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IOrdenRepository
    {
        BaseResponse Insert(EntityOrden orden);
    }
}
