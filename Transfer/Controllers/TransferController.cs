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

namespace Transfer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        private readonly ILogger<TransferController> logger;
        private readonly ITransferService service;
        private readonly IMapper mapper;

        public TransferController(ILogger<TransferController> logger,
            ITransferService service,
            IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Transfer.Models.Transfer model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var savedTransfer = service.Add(model); //AddAsync ?

                    if (savedTransfer != null)
                    {
                        return Created($"/api/partner/{savedTransfer.Id}", savedTransfer);
                    }
                    else
                    {
                        logger.LogError("savedTransfer was null");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    logger.LogError("ModelState is not valid");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save new transfer: {ex}");
                return BadRequest($"Failed to save new transfer beacuse of exception ");
            }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}
