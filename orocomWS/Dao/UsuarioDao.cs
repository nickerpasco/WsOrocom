using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient; 
using System.Linq; 
using System.Diagnostics;
using System.Data.Entity.Validation;
using orocomWS.Utils;
using orocomWS.Models;

namespace WsComercialApp.Controllers
{
    public class UsuarioDao
    {

        public ModelUsuario GetUserLogin(string queryname, List<SqlParameter> parameters)
        {
            ModelUsuario resul = (ModelUsuario)UtilsDAO.getDataObjectByQueryWithParameters<ModelUsuario>(queryname, parameters);

            return resul;

        }

        
      

        public void LogginSessionControl (ModelUsuario usuario, String SesionFlag)
        {

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.UpdateSession"); 
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@SesionFlag", SesionFlag));
            parametros.Add(new SqlParameter("@Usuario", usuario.Usuario));
            UtilsDAO.ExecuteQueryCRUD(sqlString, parametros); 

        }

         
    }
}

