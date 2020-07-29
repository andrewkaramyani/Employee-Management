using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet_Core.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger logger)
        {
            this.logger = logger;
        }
        // GET: /<controller>/
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int StatusCode)
        {
            switch (StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you request not be found ";
                    break;

            }

            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var Exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogWarning($"{Exception.Path} {Exception.Error.Message} {Exception.Error.StackTrace}");
            //ViewBag.Path = Exception.Path;
            //ViewBag.Message = Exception.Error.Message;
            //ViewBag.StackTrace = Exception.Error.StackTrace;
            return View("Error");
        }
    }
}
