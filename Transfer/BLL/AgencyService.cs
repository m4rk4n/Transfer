using AutoMapper;
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
    public class AgencyService : IAgencyService
    {
        private readonly ILogger<AgencyService> logger;
        private readonly TransferContext ctx;
        private readonly IMapper mapper;

        public AgencyService(ILogger<AgencyService> logger,
                              TransferContext ctx,
                              IMapper mapper)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public Agency Add(Agency entity)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                uow.agencyRepository.Add(entity);

                if (uow.Complete())
                {
                    logger.LogInformation("new agency created");
                    return entity;
                }
                else
                {
                    logger.LogError("new agency has not been created");
                    return null;
                }
            }
        }

        // this one is not nedeed?
        public IEnumerable<AgencyPartnersViewModel> GetAgencyPartners(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                List<AgencyPartnersViewModel> lst = new List<AgencyPartnersViewModel>();

                var allPartners = uow.partnerRepository.GetAll();
                var agency = uow.agencyRepository.GetById((int)id);
                var partnersIds = agency.AgencyPartners.ToList();

                List<Partner> partners = new List<Partner>();
                foreach (var partner in partnersIds)
                {
                    partners.Add(partner.Partner); //hmm repo
                }
                var notAgencyPartners = allPartners.Except(partners);

                foreach (var p in notAgencyPartners)
                {
                    var d = new AgencyPartnersViewModel
                    {
                        FromAgency = false,
                        PartnerId = p.Id,
                        Name = p.Name
                    };
                    lst.Add(d);
                }

                foreach (var p in partners)
                {
                    var d = new AgencyPartnersViewModel
                    {
                        FromAgency = true,
                        PartnerId = p.Id,
                        Name = p.Name
                    };
                    lst.Add(d);
                }
                return lst;
            }
        }

        public IEnumerable<Partner> GetAllPartners()
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.partnerRepository.GetAll();
            }
        }

        public void AddPartnerToAgency(object agencyId, object partnerId)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var agency = uow.agencyRepository.GetById(partnerId);
                agency.AgencyPartners.Add(
                    new AgencyPartner
                    {
                        PartnerId = (int)partnerId,
                        AgencyId = (int)agencyId
                    });
                uow.Complete();
            }
        }

        public IEnumerable<Agency> GetAll()
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.agencyRepository.GetAll();
            }
        }

        public Agency GetById(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                return uow.agencyRepository.GetById(id);
            }
        }

        public void Remove(object id)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                var entity = uow.agencyRepository.GetById(id);
                uow.agencyRepository.Delete(entity);
                uow.Complete();
            }
        }

        public Agency Update(AgencyUpdateViewModel model, object agencyId)
        {
         //  var oldAgencyPartners = GetAgencyPartners(agencyId).ToList(); // cant do this because context will get disposed
            using (var uow = new UnitOfWork(ctx))
            {
                // partners before update
                List<AgencyPartnersViewModel> oldPartners = new List<AgencyPartnersViewModel>();
                var allPartners = uow.partnerRepository.GetAll();
                var agency = uow.agencyRepository.GetById((int)agencyId);
                var partnersIds = agency.AgencyPartners.ToList();
                List<Partner> partners = new List<Partner>();
                foreach (var partner in partnersIds)
                {
                    partners.Add(partner.Partner);
                }
                var notAgencyPartners = allPartners.Except(partners);

                foreach (var p in notAgencyPartners)
                {
                    var d = new AgencyPartnersViewModel
                    {
                        FromAgency = false,
                        PartnerId = p.Id,
                        Name = p.Name
                    };
                    oldPartners.Add(d);
                }

                foreach (var p in partners)
                {
                    var d = new AgencyPartnersViewModel
                    {
                        FromAgency = true,
                        PartnerId = p.Id,
                        Name = p.Name
                    };
                    oldPartners.Add(d);
                }
                // partners before update
                foreach (var newAgencyPartner in model.AgencyPartners)
                {
                    var oldAgencyPartner = oldPartners.First(id => id.PartnerId == newAgencyPartner.PartnerId);
                    if (oldAgencyPartner != null) //if this is not new agencyPartner, maybe this if statement is not needed
                    {
                        if (oldAgencyPartner.FromAgency != newAgencyPartner.FromAgency) // if there is a difference from the old join table
                        {
                            if (newAgencyPartner.FromAgency == true) // partner is added to agency
                            {
                                agency.AgencyPartners.Add(
                                    new AgencyPartner
                                    {
                                        PartnerId = (int)newAgencyPartner.PartnerId,
                                        AgencyId = (int)agencyId
                                    });
                            }
                            else //partner is removed from agency
                            {
                                // delete a  join table entry
                                var apToRemove = agency.AgencyPartners.SingleOrDefault(ap => ap.PartnerId == newAgencyPartner.PartnerId);
                                agency.AgencyPartners.Remove(apToRemove);
                            }
                        }
                        else // there is no difference in join table entry
                        {
                            continue;
                        }
                    }
                }

                var updated = uow.agencyRepository.Update(mapper.Map<Agency>(model.Agency), model.Agency.Id);
                uow.Complete();
                return updated;
            }
        }
    }
}
