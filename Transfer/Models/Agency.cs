using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Models
{
    public class Agency
    {
        public Agency()
        {
            AgencyPartners = new List<AgencyPartner>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public ICollection<AgencyPartner> AgencyPartners {get; set;}
        

      //  public ICollection<Partner> Partners { get; set; }
    }
}
