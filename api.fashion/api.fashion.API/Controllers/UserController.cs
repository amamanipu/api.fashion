using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using api.fashion.API.Security;

namespace api.fashion.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {

        /// <summary>
        /// Constructor
        /// </summary>
        protected readonly IUserRepository _UserRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserRepository"></param>
        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(EntityLogin login)
        {
            var ret = _UserRepository.Login(login);

            if (ret.data != null)
            {
                var responseLogin = ret.data as EntityLoginResponse;
                var usercod = responseLogin.id_usuario.ToString();
                var userdoc = responseLogin.documentoidentidad;

                var token = JsonConvert.DeserializeObject<AccessToken>(
                   await new Authentication().GenerateToken(userdoc, usercod)
                ).access_token;

                responseLogin.token = token;
                ret.data = responseLogin;
            }

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Authorize]
        [HttpPost]
        [Route("insert")]
        public ActionResult Insert(EntityUser user)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            var usercod = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;
            var userdoc = claims.Where(p => p.Type == "client_numero_documento").FirstOrDefault()?.Value;

            user.UsuarioCrea = int.Parse(usercod);

            var ret = _UserRepository.Insert(user);
            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("insertclient")]
        public ActionResult InsertClient(EntityUser user)
        {
            var ret = _UserRepository.Insert(user);
            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Authorize]
        [HttpGet]
        [Route("getuser")]
        public ActionResult GetUser(int id)
        {
            var ret = _UserRepository.GetUsuarioById(id);
            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        [Produces("application/json")]
        [Authorize]
        [HttpGet]
        [Route("getusers")]
        public ActionResult GetUsers()
        {
            var ret = _UserRepository.GetUsuarios();
            return Json(ret);
        }
    }
}