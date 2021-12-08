using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        public BaseResponse GetImagesByProductDeail(int id_producto_detalle)
        {
            var returnEntity = new BaseResponse();
            var entitiesImage = new List<EntityImage>();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ObtenerProductoDetalleImagen";

                    var p = new DynamicParameters();
                    p.Add("@ID_PRODUCTO_DETALLE", value: id_producto_detalle, DbType.Int32, direction: ParameterDirection.Input);

                    entitiesImage = db.Query<EntityImage>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    if (entitiesImage.Count > 0)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errorcode = string.Empty;
                        returnEntity.data = entitiesImage;

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

        public BaseResponse Insert(EntityImage image)
        {
            var returnEntity = new BaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_InsertarProductoDetalleImagen";
                    var p = new DynamicParameters();
                    p.Add("@ID_IMAGEN", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@ID_PRODUCTO_DETALLE", value: image.id_producto_detalle, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@NOMBRE", value: image.nombre, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@RUTA", value: image.ruta, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@TIPO", value: image.tipo, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@USUARIOCREA", value: image.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityImage>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int id_imagen = p.Get<int>("@ID_IMAGEN");

                    if (id_imagen > 0)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new { id_imagen = id_imagen };
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
