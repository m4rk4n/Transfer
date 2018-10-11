using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.ViewModels
{
    public class PartnerUpdateViewModel
    {
        public PartnerViewModel Partner { get; set; }
        public IEnumerable<PartnerAgenciesViewModel> PartnerAgencies { get; set; }
        public int? VehicleId { get; set; }
    }
}
