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
        IEnumerable<PartnerAgenciesViewModel> GetPartnerAgencies(object id);
        Partner GetById(object id);
        Partner Add(Partner entity, int? VehicleId);
        Partner Update(PartnerUpdateViewModel model, object partnerId);
        void Remove(object id);
    }
}
