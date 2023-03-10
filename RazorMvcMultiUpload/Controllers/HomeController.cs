using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorMvcMultiUpload.Models;

namespace RazorMvcMultiUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 409715200)]
        [RequestSizeLimit(409715200)]
        [Obsolete]
        public IActionResult Upload(IFormFile file, [FromServices] IHostingEnvironment ohostingEnvironment)
        {
            string fileName = $"{ohostingEnvironment.WebRootPath}//UploadFolder//{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            ViewData["message"] = $"File uploaded Successful. File Length : (file.Length) bytes";


            return View("Index");
            

        }


    }
}
