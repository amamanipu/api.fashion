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
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IProductRepository _ProductRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductRepository"></param>
        public ProductController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("getproducts")]
        public ActionResult GetProducts()
        {
            var ret = _ProductRepository.GetProducts();
            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("getproduct")]
        public ActionResult GetProduct(int id)
        {
            var ret = _ProductRepository.GetProductById(id);
            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [Authorize]
        [HttpPost]
        [Route("insert")]
        public ActionResult Insert(EntityProduct product)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            var usercod = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;
            var userdoc = claims.Where(p => p.Type == "client_numero_documento").FirstOrDefault()?.Value;

            product.UsuarioCrea = int.Parse(usercod);

            var ret = _ProductRepository.Insert(product);
            return Json(ret);
        }
    }
}
