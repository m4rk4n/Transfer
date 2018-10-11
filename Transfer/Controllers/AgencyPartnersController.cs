using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.BLL.Interfaces;
using Transfer.ViewModels;

namespace Transfer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class AgencyPartnersController : Controller
    {
        private readonly ILogger<AgencyPartnersController> logger;
        private readonly IAgencyService service;
        private readonly IMapper mapper;

        public AgencyPartnersController(ILogger<AgencyPartnersController> logger,
                                IAgencyService service,
                                IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

         [HttpGet("{id}")]
         public ActionResult<AgencyPartnersViewModel> Partners(int id)
        {
            return Ok(service.GetAgencyPartners(id));
        }
    }
}
