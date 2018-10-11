using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;
using Transfer.ViewModels;

namespace Transfer.BLL.Interfaces
{
    public interface IAgencyService
    {
        IEnumerable<Agency> GetAll();
        Agency GetById(object id);
        Agency Add(Agency entity);
        Agency Update(AgencyUpdateViewModel model, object agencyId);
        void Remove(object id);
        IEnumerable<AgencyPartnersViewModel> GetAgencyPartners(object id);
    }
}
