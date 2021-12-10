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
    [Route("api/contact")]
    [ApiController]
    public class ContactController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IContactRepository _ContactRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContactRepository"></param>
        public ContactController(IContactRepository ContactRepository)
        {
            _ContactRepository = ContactRepository;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        [Route("insert")]
        public ActionResult Insert(EntityContact contact)
        {
            var ret = _ContactRepository.Insert(contact);
            return Json(ret);
        }

    }
}
