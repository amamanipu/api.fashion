using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace DBContext
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public BaseResponse Insert(EntityContact contact)
        {
            var returnEntity = new BaseResponse();
            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_InsertarContacto";

                    var p = new DynamicParameters();
                    p.Add(name: "@IDCONTACTO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add(name: "@NOMBRES", value: contact.nombres, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@CORREO", value: contact.correo, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@COMENTARIO", value: contact.comentarios, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@USUARIOCREA", value: 1, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityUser>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int idcontacto = p.Get<int>("@IDCONTACTO");

                    if (idcontacto > 0)
                    {
                        returnEntity.issuccess = true;
                        returnEntity.errorcode = "0000";
                        returnEntity.errormessage = string.Empty;
                        returnEntity.data = new
                        {
                            id = idcontacto,
                            nombre = contact.nombres
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

    }
}
