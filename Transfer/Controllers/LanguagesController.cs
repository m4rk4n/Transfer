using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Controllers
{
    [Route("api/[controller]")]
    public class LanguagesController : Controller
    {
        public LanguagesController()
        {
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var file = Path.Combine(
                         Directory.GetCurrentDirectory(), "wwwroot", "Resources", id);

            return PhysicalFile(file, "application/json");
        }
    }
}
