using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IProductDetailRepository
    {
        BaseResponse GetDetailByProduct(int id_producto);

        BaseResponse Insert(EntityProductDetail productDetail);
    }
}
