using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public BaseResponse Delete(int id)
        {
            var returnEntity = new BaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_EliminarProducto";
                    var p = new DynamicParameters();
                    p.Add("@ID_PRODUCTO", value: id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    
                    db.Query<EntityProduct>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    if (id > 0)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new { id = id };
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

        public BaseResponse GetProductById(int id)
        {
            var returnEntity = new BaseResponse();
            var entityProduct = new EntityProduct();
            var productDetailRepository = new ProductDetailRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ObtenerProducto";

                    var p = new DynamicParameters();
                    p.Add("@ID_PRODUCTO", value: id, DbType.Int32, direction: ParameterDirection.Input);

                    entityProduct = db.Query<EntityProduct>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    if (entityProduct != null)
                    {
                        entityProduct.productoDetalles = productDetailRepository.GetDetailByProduct(id).data as List<EntityProductDetail>;

                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = entityProduct;
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

        public BaseResponse GetProducts()
        {
            var returnEntity = new BaseResponse();
            var entitiesProduct = new List<EntityProduct>();
            var productDetailRepository = new ProductDetailRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ListarProductos";

                    entitiesProduct = db.Query<EntityProduct>(
                        sql: sql,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    if (entitiesProduct.Count > 0)
                    {
                        foreach (var product in entitiesProduct)
                        {
                            product.productoDetalles = productDetailRepository.GetDetailByProduct(product.id_producto).data as List<EntityProductDetail>;
                        }

                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errorcode = string.Empty;
                        returnEntity.data = entitiesProduct;

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

        public BaseResponse Insert(EntityProduct product)
        {
            var returnEntity = new BaseResponse();
            var productDetailRepository = new ProductDetailRepository();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_InsertarProducto";
                    var p = new DynamicParameters();
                    p.Add("@ID_PRODUCTO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@NOMBRE", value: product.nombre, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@DESCRIPCION", value: product.descripcion, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@ID_CATEGORIA", value: product.id_categoria, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@ID_MARCA", value: product.id_marca, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@PRECIO", value: product.precio, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                    p.Add("@USUARIOCREA", value: product.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityProduct>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int id_producto = p.Get<int>("@ID_PRODUCTO");

                    if (id_producto > 0)
                    {
                        foreach (var productoDetalle in product.productoDetalles)
                        {
                            productoDetalle.id_producto = id_producto;
                            productoDetalle.UsuarioCrea = product.UsuarioCrea;
                            EntityProductDetail entityProductDetail = productDetailRepository.Insert(productoDetalle).data as EntityProductDetail;
                        }

                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new { id = id_producto, nombre = product.nombre };
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

        public BaseResponse Update(EntityProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
