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
using Transfer.DAL;
using Transfer.Models;
using Transfer.ViewModels;

namespace Transfer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> logger;
        private readonly IVehicleService service;
        private readonly IMapper mapper;
        private readonly TransferContext ctx;

        public VehicleController(ILogger<VehicleController> logger,
                                 IVehicleService service,
                                 IMapper mapper,
                                 TransferContext ctx)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
            this.ctx = ctx;
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Post([FromBody]Vehicle model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Vehicle newVehicle = new Vehicle
                    {
                        Name = model.Name,
                        PlateNumber = model.PlateNumber,
                        RegistrationDate = model.RegistrationDate,
                        Note = model.Note,
                    };                    

                    using (var uow = new UnitOfWork(this.ctx))
                    {
                        uow.vehicleRepository.Add(newVehicle);
                        if (uow.CompleteAsync().Result)
                        // or if (uow.Complete())
                        {
                            logger.LogInformation("new vehicle created");
                            return Created($"/api/vehicle/{newVehicle.Id}", newVehicle);
                        }
                        else
                        {
                            logger.LogError("Bad request");
                            return BadRequest(ModelState);
                        }
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save new vehicle: {ex}");
                return BadRequest($"Failed to save new vehicle beacuse of exception, end user better not see this one :)");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<VehicleViewModel>> Get()
        {
            var vehicles = service.GetAll();
            if (vehicles != null)
            {
                return Ok(mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleViewModel>>(vehicles));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<VehicleViewModel> Get(int id)
        {
            var vehicle = service.GetById(id);
            if (vehicle != null)
            {
                return Ok(mapper.Map<Vehicle, VehicleViewModel>(vehicle));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PartnerViewModel> Put(int id, [FromBody] VehicleViewModel model)
        {
            var updatedVehicle = service.Update(mapper.Map<Vehicle>(model), model.Id);
            if (updatedVehicle != null)
            {
                logger.LogInformation("Vehicle updated");
                return Ok(updatedVehicle);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            service.Remove(id);
            return NoContent(); // kako ovdje provjeru 
        }
    }
}
