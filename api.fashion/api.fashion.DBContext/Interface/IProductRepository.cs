using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IProductRepository
    {
        BaseResponse GetProducts();
        BaseResponse GetProductById(int id);
        BaseResponse Insert(EntityProduct product);
        BaseResponse Update(EntityProduct product);
        BaseResponse Delete(int id);
    }
}
