using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.ViewModels
{
    public class AgencyUpdateViewModel
    {
        public AgencyWithIdViewModel Agency { get; set; }
        public IEnumerable<AgencyPartnersViewModel> AgencyPartners { get; set; }
    }
}
