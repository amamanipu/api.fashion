using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public BaseResponse GetUsuarioById(int id)
        {
            var returnEntity = new BaseResponse();
            var entityUser = new EntityUser();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ObtenerUsuario";

                    var p = new DynamicParameters();
                    p.Add("@ID_USUARIO", value: id, DbType.Int32, direction: ParameterDirection.Input);

                    entityUser = db.Query<EntityUser>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    if (entityUser != null)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = entityUser;
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

        public BaseResponse GetUsuarios()
        {
            var returnEntity = new BaseResponse();
            var entitiesUser = new List<EntityUser>();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_ListarUsuarios";

                    entitiesUser = db.Query<EntityUser>(
                        sql: sql,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    if (entitiesUser.Count > 0)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = entitiesUser;
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

        public BaseResponse Insert(EntityUser user)
        {
            var returnEntity = new BaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_InsertarUsuario";

                    var p = new DynamicParameters();
                    p.Add(name: "@IDUSUARIO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add(name: "@EMAIL", value: user.email, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PASSWORD", value: user.password, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@IDPERFIL", value: user.id_perfil, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@NOMBRES", value: user.nombres, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@APELLIDOS", value: user.apellidos, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@DOCUMENTOIDENTIDAD", value: user.documentoidentidad, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@DIRECCION", value: user.direccion, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@CELULAR", value: user.celular, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@USUARIOCREA", value: user.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityUser>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int idusuario = p.Get<int>("@IDUSUARIO");

                    if (idusuario > 0)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new
                        {
                            id = idusuario,
                            nombre = user.nombres + " " + user.apellidos
                        };
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

        public BaseResponse Login(EntityLogin login)
        {
            var returnEntity = new BaseResponse();
            var entityLoginResponse = new EntityLoginResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_user_login";

                    var p = new DynamicParameters();
                    p.Add(name: "@EMAIL", value: login.email, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PASSWORD", value: login.password, dbType: DbType.String, direction: ParameterDirection.Input);

                    entityLoginResponse = db.Query<EntityLoginResponse>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    if (entityLoginResponse != null)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = entityLoginResponse;
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
