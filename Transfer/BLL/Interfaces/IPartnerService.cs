using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;
using Transfer.ViewModels;

namespace Transfer.BLL.Interfaces
{
    public interface IPartnerService
    {
        IEnumerable<Partner> GetAll();
        Partner GetById(object id);
        Partner Add(Partner entity, int? VehicleId);
        Partner Update(PartnerUpdateViewModel model, object partnerId);
        void Remove(object id);
        IEnumerable<PartnerAgenciesViewModel> GetPartnerAgencies(object id);
       // bool UpdatePartnerAgencies(List<PartnerAgenciesViewModel> lst, object agencyId);
    }
}
