using System.Collections.Generic;
using System.IO;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet]
        [Route("GenerateClientReport")]

        public IActionResult GenerateClientReport()
        {
            return null;
        }


    }
}
