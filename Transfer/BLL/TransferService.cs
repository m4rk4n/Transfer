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
    public class TransferService : ITransferService
    {

        private readonly ILogger<TransferService> logger;
        private readonly TransferContext ctx;

        public TransferService(ILogger<TransferService> logger,
                              TransferContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
        }

        public Models.Transfer Add(Models.Transfer entity)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                if (entity.Agency != null)
                    entity.AgencyId = entity.Agency.Id;
                entity.Agency = null; // in order not to save related entities in db because they already exist // i don't need these nulls because i don't work with enities
                entity.PartnerId = entity.Partner.Id;
                var vehicle = uow.vehicleRepository.GetById(entity.Partner.VehicleId);
                entity.Partner = null;
                entity.VehicleId = vehicle.Id;

                uow.transferRepository.Add(entity);
                
                if (uow.Complete())
                {
                    logger.LogInformation("new transfer created");
                    return entity;
                }
                else
                {
                    logger.LogError("new transfer has not been created");
                    return null;
                }
            }
        }

        public IEnumerable<Models.Transfer> GetAll()
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.transferRepository.GetAll();
            }
        }

        public Models.Transfer GetById(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.transferRepository.GetById(id);
            }
        }

        public void Remove(Models.Transfer entity)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                uow.transferRepository.Delete(entity);
            }
        }

        public Models.Transfer Update(Models.Transfer entity, object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var updated = uow.transferRepository.Update(entity, id);
                uow.Complete();
                return updated;
            }
        }
    }
}
