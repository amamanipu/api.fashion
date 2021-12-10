using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
    public class OrdenRepository : BaseRepository, IOrdenRepository
    {
        public BaseResponse Insert(EntityOrden orden)
        {
            var returnEntity = new BaseResponse();
            var imageRepository = new ImageRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_Orden";
                    var p = new DynamicParameters();
                    p.Add("@ACCION", value: "INSERT_ORDEN", dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@ID_ORDEN", value: orden.ID_ORDEN, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    p.Add("@ID_CLIENTE", value: orden.ID_CLIENTE, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@FEC_REGISTRO", value: orden.FEC_REGISTRO, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@FEC_ENTREGA", value: orden.FEC_ENTREGA, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@METODO_PAGO", value: orden.METODO_PAGO, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@DIRECCION_ENTREGA", value: orden.DIRECCION_ENTREGA, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@REFERENCIA_ENTREGA", value: orden.REFERENCIA_ENTREGA, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@IMP_TOTAL", value: orden.IMP_TOTAL, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@ESTADO_ORDEN", value: orden.ESTADO_ORDEN, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@ID_PRODUCTO", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@CANTIDAD", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@PRECIO", value: 0, dbType: DbType.Double, direction: ParameterDirection.Input);

                    //db.Query<EntityProductDetail>(
                    //    sql: sql,
                    //    param: p,
                    //    commandType: CommandType.StoredProcedure
                    //).FirstOrDefault();

                    Int64 _id = db.ExecuteScalar<Int64>(sql, p, commandType: CommandType.StoredProcedure);

                    //int id_producto_detalle = p.Get<int>("@ID_PRODUCTO_DETALLE");

                    if (_id > 0)
                    {
                        foreach (var det in orden.detalle)
                        {
                            p.Add("@ACCION", "INSERT_DETALLE");
                            p.Add("@ID_ORDEN", _id);
                            p.Add("@ID_PRODUCTO", det.ID_PRODUCTO);
                            p.Add("@CANTIDAD", det.cantidad);
                            p.Add("@PRECIO", det.precio);

                            db.Query<EntityProduct>(
                                sql: sql,
                                param: p,
                                commandType: CommandType.StoredProcedure
                            ).FirstOrDefault();
                        }

                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new { id = _id };
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
