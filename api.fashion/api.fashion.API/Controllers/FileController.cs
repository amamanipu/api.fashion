using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.IO;
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
    [Route("api/file")]
    [ApiController]
    public class FileController : Controller
    {
        private string basePath;

        /// <summary>
        /// 
        /// </summary>
        public FileController()
        {
            //this.basePath = "C:\\Fashion21\\productos";
            this.basePath = "Resources";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [RequestSizeLimit(long.MaxValue)]
        [HttpPost]
        [AllowAnonymous]
        [Route("upload")]
        public async Task<ActionResult> upload(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            //C:\\ArchivosPrueba\uploads
            var uploadPath = Path.Combine(basePath, "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), uploadPath);
            var fullPath = Path.Combine(pathToSave, file.FileName);
            var dbPath = Path.Combine(uploadPath, file.FileName);

            //if (!Directory.Exists(uploadPath))
            //{
            //    Directory.CreateDirectory(uploadPath);
            //}

            //C:\\ArchivosPrueba\uploads\archivo.txt
            var filePath = Path.Combine(uploadPath, file.FileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok();
        }
    }
}
