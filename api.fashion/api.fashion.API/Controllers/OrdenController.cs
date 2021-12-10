using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.fashion.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orden")]
    [ApiController]
    public class OrdenController : Controller
    {
        protected readonly IOrdenRepository _OrdenRepository;
        public OrdenController(IOrdenRepository OrdenRepository)
        {
            _OrdenRepository = OrdenRepository;
        }

        [Produces("application/json")]
        //[Authorize]
        [HttpPost]
        [Route("insert")]
        public ActionResult Insert(EntityOrden orden)
        {
            // hola este es mi codigo pero no salio en el github (nhidalgo)
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            var usercod = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;
            var userdoc = claims.Where(p => p.Type == "client_numero_documento").FirstOrDefault()?.Value;

            //product.UsuarioCrea = int.Parse(usercod);

            var ret = _OrdenRepository.Insert(orden);
            return Json(ret);
        }
    }
}
