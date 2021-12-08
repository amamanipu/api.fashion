using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IImageRepository
    {
        BaseResponse GetImagesByProductDeail(int id_producto_detalle);

        BaseResponse Insert(EntityImage image);
    }
}
