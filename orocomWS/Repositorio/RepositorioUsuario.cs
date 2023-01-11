using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO; 
using System.Linq;
using WsComercialApp;
using orocomWS.Models;
using orocomWS.Utils;
using WsComercialApp.Controllers;
using orocomWS.Models.Bd;
using System.Data.Entity;
using System.Web;

namespace orocomWS.Controllers
{
    public class RepositorioUsuario
    {
        public List<ModelAsistencia> ListarAsistencias(ModelUsuario bean)
        {
            List<ModelAsistencia> response = new List<ModelAsistencia>();



            UsuarioDao dao = new UsuarioDao();

            var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.getListarAsistencias");

            List<SqlParameter> parametrosUserPass = new List<SqlParameter>();
            parametrosUserPass.Add(new SqlParameter("@Persona", bean.Persona));
            //parametrosUserPass.Add(new SqlParameter("@FechaInicio", bean.FechaInicio)); 
            //parametrosUserPass.Add(new SqlParameter("@FechaFin", bean.FechaFin)); 

            response = UtilsDAO.getDataByQueryWithParameters<ModelAsistencia>(sqlString, parametrosUserPass);



            return response;

        }
        public ModelUsuario GetAllByID(ModelUsuario bean)
        {
            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();


            if (bean == null)
            {
                response = new ModelUsuario();
                error.CodigoError = 500;
                error.MensajeError = "Verifique el objecto Request, es nulo";
                response.lstErrores.Add(error);
                return response;
            }

            try
            {

                bean = InitFuncClave(bean); // ENCRIPTAR CLAVE      

                UsuarioDao dao = new UsuarioDao();

                var sqlString = UtilsGlobal.ConvertLinesSqlXml("Query_Usuario", "Usuario.GetAllParameters");

                List<SqlParameter> parametrosUserPass = new List<SqlParameter>();
                parametrosUserPass.Add(new SqlParameter("@UserName", bean.Usuario));


                var dataValidaUsuario = dao.GetUserLogin(sqlString, parametrosUserPass);

                if (dataValidaUsuario == null)
                {
                    response = new ModelUsuario();
                    error.CodigoError = 500;
                    error.MensajeError = "El Usuario : " + bean.Usuario + " no está registrado";
                    response.lstErrores.Add(error);
                    return response;
                }

                response = ValidarExistenciaUsuario(dataValidaUsuario, bean.Clave);


                if (response.lstErrores.Count > 0)
                {
                    return response;
                }

                ModelUsuario userpass = response;


                if (userpass != null)
                {


                    if (userpass.EstadoEmpleado == "I")
                    {
                        response = new ModelUsuario();
                        error.CodigoError = 500;
                        error.MensajeError = "La persona asociada al usuario : " + bean.Usuario + ", se encuentra inactivo.";
                        response.lstErrores.Add(error);
                        return response;
                    }


                    if (userpass.FechaCeseEmpleado != null)
                    {
                        response = new ModelUsuario();
                        error.CodigoError = 500;
                        error.MensajeError = "El empleado asociado al usuario : " + bean.Usuario + ", se encuentra Cesado desde : " + userpass.FechaCeseEmpleado;
                        response.lstErrores.Add(error);
                        return response;
                    }

                }
                else
                {
                    response = new ModelUsuario();
                    error.CodigoError = 500;
                    error.MensajeError = "Verifique el usuario en las siguentes tablas : EmpleadoMast,PersonaMast,FT_Vehiculo_Conductor,FT_Vehiculo_Detalle";
                    response.lstErrores.Add(error);
                    return response;
                }


                //List<ModelParametros> parametrosSistema = dao.getParametrosSistema(response);
                //List<ModelMiscelaneos> miscelaneosAll = dao.getMiscelaneos(response);

                //var objttoken = TokenGenerator.GenerateTokenJwt(FuncPrinc.trimValor(response.Usuario));
                //response.Token = objttoken.Token;
                //response.TokenFechaExpiracion = objttoken.TokenFechaExpiracion;
                //response.TokenFechaExpiracionString = objttoken.TokenFechaExpiracion.ToString("dd/MM/yyyy h:mm tt");
                //response.lstparametros = parametrosSistema;
                //response.lstmiscelaneos = miscelaneosAll;
                //response.lstSeguridad = dataSeguridad; 
                return response;

            }
            catch (Exception e)
            {

                error.CodigoError = 500;
                error.MensajeError = e.Message;
                response.lstErrores.Add(error);
                return response;
            }

            return response;

        }


        public AsistenciaDiariaMarcas SaveAsistencia(AsistenciaDiariaMarcas objSC)
        {
            using (BdEntityGenerico context = new BdEntityGenerico())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    {
                        try
                        {

                            DateTime? now = DateTime.Now;
                            string DateFecha = now.HasValue ? now.Value.ToString("yyyyMMdd") : string.Empty;

                            var maxId = context.AsistenciaDiariaMarcas.DefaultIfEmpty().Max(t => t == null ? 1 : t.Secuencia);
                            var ip = "104.211.62.150"; //HttpContext.Current.Request.UserHostAddress; ;


                            objSC.Secuencia = maxId+1;
                            objSC.IpCreacion = ip;
                            objSC.FechaCreacion = DateTime.Now; 
                            

                            context.Entry(objSC).State = EntityState.Added;
                            context.SaveChanges();


                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }

            return objSC;

        }


        private ModelUsuario ValidarExistenciaUsuario(ModelUsuario bean, String clavedigitada)
        {

            ModelUsuario response = new ModelUsuario();
            ErrorObj error = new ErrorObj();

            bean.Usuario = FuncPrinc.trimValor(bean.Usuario);

            if (bean == null)
            {
                response = new ModelUsuario();
                error.CodigoError = 500;
                error.MensajeError = "El Usuario : " + bean.Usuario + " no está registrado";
                response.lstErrores.Add(error);
                return response;
            }
            else
            {
                String claveDescripciptada = "" + FuncPrinc.trimValor(bean.Clave);// FuncPrinc.springDesencriptar(FuncPrinc.trimValor(response.Clave).ToUpper());
                String claveingresada = "" + FuncPrinc.trimValor(clavedigitada);// FuncPrinc.trimValor(bean.Clave).ToUpper();



                if (claveDescripciptada != claveingresada)
                {
                    error.CodigoError = 800;
                    error.MensajeError = "La contraseña ingresada, no coincide con el Usuario : " + bean.Usuario;
                    response.lstErrores.Add(error);
                    return response;
                }


                if (bean.Estado != "A")
                {


                    error.CodigoError = 800;
                    error.MensajeError = "El Usuario : " + bean.Usuario + " está inactivo";
                    response.lstErrores.Add(error);
                    return response;
                }


                if (bean.SesionFlag == "S")
                {
                    error.CodigoError = 1000;
                    error.MensajeError = "El Usuario : " + bean.Usuario + ", Ya se encuentra activo en otro dispositivo.";
                    response.lstErrores.Add(error);
                    //return response;
                }

            }

            return bean;
        }


        public ModelUsuario InitFuncClave(ModelUsuario data)
        {
            ModelUsuario returndata = new ModelUsuario();
            String claveenciptada = FuncPrinc.springEncriptar(data.Clave);
            //String descriptar = FuncPrinc.springDesencriptar(data.Clave);

            returndata.Usuario = data.Usuario;
            returndata.Clave = claveenciptada;

            return returndata;
        }
    }
}

