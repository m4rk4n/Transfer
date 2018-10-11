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
    public class PartnerAgenciesController : Controller
    {
        private readonly ILogger<PartnerAgenciesController> logger;
        private readonly IPartnerService service;
        private readonly IMapper mapper;

        public PartnerAgenciesController(ILogger<PartnerAgenciesController> logger,
                                IPartnerService service,
                                IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<AgencyPartnersViewModel> Partners(int id)
        {
            return Ok(service.GetPartnerAgencies(id));
        }
    }
}
