using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.BLL.Interfaces;
using Transfer.DAL;
using Transfer.Models;

namespace Transfer.BLL
{
    public class VehicleService : IVehicleService
    {

        private readonly ILogger<VehicleService> logger;
        private readonly TransferContext ctx;

        public VehicleService(ILogger<VehicleService> logger,
                              TransferContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
        }

        public Vehicle Add(Vehicle entity) // generic service, reflection
        {
            using (var uow = new UnitOfWork(ctx))
            {
                uow.vehicleRepository.Add(entity);

                if (uow.Complete())
                {
                    logger.LogInformation("new vehicle created");
                    return entity;
                }
                else
                {
                    logger.LogError("new vehicle has not been created");
                    return null;
                }
            }
        }

        public IEnumerable<Vehicle> GetAll()
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.vehicleRepository.GetAll();
            }
        }

        public Vehicle GetById(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.vehicleRepository.GetById(id);
            }
        }

        public void Remove(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var entity = uow.vehicleRepository.GetById(id);
                uow.vehicleRepository.Delete(entity);
                uow.Complete();
            }
        }

        public Vehicle Update(Vehicle entity, object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var updated = uow.vehicleRepository.Update(entity, id);
                uow.Complete();
                return updated;
            }
        }
    }
}
