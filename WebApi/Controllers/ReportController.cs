using System.Collections.Generic;
using System.IO;
using BL.Services;
using BL.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet]
        [Route("GenerateClientReport")]

        public IActionResult GenerateClientReport()
        {
            var file = _reportService.GenerateClientReport();
            Stream stream = new MemoryStream(file);
            return File(stream, "application/pdf", "testeReport.pdf");
           
        }


    }
}
