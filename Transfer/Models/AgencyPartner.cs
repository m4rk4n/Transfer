using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Models
{
    public class AgencyPartner
    {
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }

        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
