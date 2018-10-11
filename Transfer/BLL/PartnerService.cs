using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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

namespace Transfer.BLL
{
    public class PartnerService : IPartnerService
    {
        private readonly ILogger<PartnerService> logger;
        private readonly TransferContext ctx;
        private readonly IMapper mapper;

        public PartnerService(ILogger<PartnerService> logger,
                              TransferContext ctx,
                              IMapper mapper)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public Partner Add(Partner entity, int? vehicleId)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                Vehicle vehicle = null;
                if (vehicleId != null)
                {
                    vehicle = uow.vehicleRepository.GetById(vehicleId);
                    entity.Vehicle = vehicle;
                }

                uow.partnerRepository.Add(entity);
                
                if (uow.Complete())
                {
                    logger.LogInformation("new partner created");
                    return entity; 
                }
                else
                {
                    logger.LogError("new partner has not been created");
                    return null;
                }
            }
        }

        public void AddAgencyToPartner(object agencyId, object partnerId)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var partner = uow.partnerRepository.GetById(partnerId);
                partner.AgencyPartners.Add(
                    new AgencyPartner {
                        PartnerId = (int)(partnerId),
                        AgencyId = (int)(agencyId)
                    });
                uow.Complete();
            }
        }

        public IEnumerable<Partner> GetAll()
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.partnerRepository.GetAll();
            }
        }

        public Partner GetById(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.partnerRepository.GetById(id);
            }
        }

        public IEnumerable<PartnerAgenciesViewModel> GetPartnerAgencies(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                List<PartnerAgenciesViewModel> lst = new List<PartnerAgenciesViewModel>();

                var allAgencies = uow.agencyRepository.GetAll();
                var partner = uow.partnerRepository.GetById((int)id);
                var agenciesIds = partner.AgencyPartners.ToList();

                List<Agency> agencies = new List<Agency>();
                foreach (var agency in agenciesIds)
                {
                    agencies.Add(agency.Agency); //hmm repo
                }
                var notPartnerAgencies = allAgencies.Except(agencies);

                foreach (var p in notPartnerAgencies)
                {
                    var d = new PartnerAgenciesViewModel
                    {
                        FromPartner = false,
                        AgencyId = p.Id,
                        Name = p.Name
                    };
                    lst.Add(d);
                }

                foreach (var p in agencies)
                {
                    var d = new PartnerAgenciesViewModel
                    {
                        FromPartner = true,
                        AgencyId = p.Id,
                        Name = p.Name
                    };
                    lst.Add(d);
                }
                return lst;
            }
        }

        public void Remove(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var entity = uow.partnerRepository.GetById(id);
                uow.partnerRepository.Delete(entity);
                uow.Complete();
            }
        }

        public Partner Update(PartnerUpdateViewModel model, object partnerId)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                // partners before update
                List<PartnerAgenciesViewModel> oldAgencies = new List<PartnerAgenciesViewModel>();
                var allAgencies = uow.agencyRepository.GetAll();
                var partner = uow.partnerRepository.GetById((int)partnerId);
                var partnerVehicle = uow.vehicleRepository.GetById(model.VehicleId);

                if (partner.Vehicle != partnerVehicle)
                {
                    partner.VehicleId = partnerVehicle.Id;
                }
                var agenciesIds = partner.AgencyPartners.ToList();
                List<Agency> agencies = new List<Agency>();
                foreach (var agency in agenciesIds)
                {
                    agencies.Add(agency.Agency);
                }
                var notPartnerAgencies = allAgencies.Except(agencies);

                foreach (var p in notPartnerAgencies)
                {
                    var d = new PartnerAgenciesViewModel
                    {
                        FromPartner = false,
                        AgencyId = p.Id,
                        Name = p.Name
                    };
                    oldAgencies.Add(d);
                }

                foreach (var p in agencies)
                {
                    var d = new PartnerAgenciesViewModel
                    {
                        FromPartner = true,
                        AgencyId = p.Id,
                        Name = p.Name
                    };
                    oldAgencies.Add(d);
                }
                // ===========================
                foreach (var newPartnerAgency in model.PartnerAgencies)
                {
                    var oldPartnerAgency = oldAgencies.First(id => id.AgencyId == newPartnerAgency.AgencyId);
                    if (oldPartnerAgency != null) //if this is not new partnerAgency, maybe this if statement is not needed
                    {
                        if (oldPartnerAgency.FromPartner != newPartnerAgency.FromPartner) // if there is a difference from the old join table
                        {
                            if (newPartnerAgency.FromPartner == true) // agency is added to partner
                            {
                                partner.AgencyPartners.Add(
                                    new AgencyPartner
                                    {
                                        PartnerId = (int)partnerId,
                                        AgencyId = (int)newPartnerAgency.AgencyId                                    });
                            }
                            else //agency is removed from partner
                            {
                                // delete a  join table entry
                                var apToRemove = partner.AgencyPartners.SingleOrDefault(ap => ap.AgencyId == oldPartnerAgency.AgencyId);
                                partner.AgencyPartners.Remove(apToRemove);
                            }
                        }
                        else // there is no difference in join table entry
                        {
                            continue;
                        }
                    }
                }

                //var updated = uow.partnerRepository.Update(mapper.Map<Partner>(model.Partner), model.Partner.Id);
                var updated = uow.partnerRepository.Update(partner, partner.Id);
                uow.Complete();
                return updated;
            }
        }

        public bool UpdatePartnerAgencies(List<PartnerAgenciesViewModel> lst, object agencyId)
        {
            throw new NotImplementedException();
        }
    }
}
