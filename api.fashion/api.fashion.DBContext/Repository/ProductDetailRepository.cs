using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class ProductDetailRepository : BaseRepository, IProductDetailRepository
    {
        public BaseResponse GetDetailByProduct(int id_producto)
        {
            var returnEntity = new BaseResponse();
            var entitiesProductDetail = new List<EntityProductDetail>();
            var imagenRepository = new ImageRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ObtenerProductoDetalle";

                    var p = new DynamicParameters();
                    p.Add("@ID_PRODUCTO", value: id_producto, DbType.Int32, direction: ParameterDirection.Input);

                    entitiesProductDetail = db.Query<EntityProductDetail>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    if (entitiesProductDetail.Count > 0)
                    {
                        foreach (var productDetail in entitiesProductDetail)
                        {
                            productDetail.imagenes = imagenRepository.GetImagesByProductDeail(productDetail.id_producto_detalle).data as List<EntityImage>;
                        }

                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errorcode = string.Empty;
                        returnEntity.data = entitiesProductDetail;

                    }
                    else
                    {
                        returnEntity.issuccess = false;
                        returnEntity.errorcode = "0000";
                        returnEntity.errorcode = string.Empty;
                        returnEntity.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                returnEntity.issuccess = false;
                returnEntity.errorcode = "0001";
                returnEntity.errorcode = ex.Message;
                returnEntity.data = null;
            }
            return returnEntity;
        }

        public BaseResponse Insert(EntityProductDetail productDetail)
        {
            var returnEntity = new BaseResponse();
            var imageRepository = new ImageRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_InsertarProductoDetalle";
                    var p = new DynamicParameters();
                    p.Add("@ID_PRODUCTO_DETALLE", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@ID_PRODUCTO", value: productDetail.id_producto, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@ID_TAMANO", value: productDetail.id_tamano, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@COLOR", value: productDetail.color, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@STOCK", value: productDetail.stock, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@USUARIOCREA", value: productDetail.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityProductDetail>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int id_producto_detalle = p.Get<int>("@ID_PRODUCTO_DETALLE");

                    if (id_producto_detalle > 0)
                    {
                        foreach (var imagen in productDetail.imagenes)
                        {
                            imagen.id_producto_detalle = id_producto_detalle;
                            imagen.UsuarioCrea = productDetail.UsuarioCrea;
                            EntityImage entityImagen = imageRepository.Insert(imagen).data as EntityImage;
                        }

                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new { id_producto_detalle = id_producto_detalle };
                    }
                    else
                    {
                        returnEntity.issuccess = false;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                returnEntity.issuccess = false;
                returnEntity.errorcode = "0001";
                returnEntity.errormessage = ex.Message;
                returnEntity.data = null;
            }

            return returnEntity;
        }
    }
}
