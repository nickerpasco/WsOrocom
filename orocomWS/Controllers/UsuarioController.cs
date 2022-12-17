using System;
using System.Web.Http;
using System.Collections.Generic; 
using Newtonsoft.Json;
using orocomWS.Models;
using orocomWS.Models.Bd;

namespace orocomWS.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET:  Usuario

        [HttpPost]
        [Route("api/Usuario/Login")]
        public ModelUsuario GetAllByID([FromBody] ModelUsuario bean)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.GetAllByID(bean);
            return resultlist;

        }

        [HttpPost]
        [Route("api/Usuario/SaveAsistencia")]
        public AsistenciaDiariaMarcas SaveAsistencia([FromBody] AsistenciaDiariaMarcas SaveAsistencia)
        {
            RepositorioUsuario usuario = new RepositorioUsuario();
            var resultlist = usuario.SaveAsistencia(SaveAsistencia);
            return resultlist;

        }

        //[HttpPost]
        //[Route("api/Usuario/CambioClave")]
        //public ModelUsuario CambioClave([FromBody] ModelUsuario bean)
        //{
        //    RepositorioUsuario usuario = new RepositorioUsuario();
        //    var resultlist = usuario.CambioClave(bean);
        //    return resultlist;

        //}






    }
}

