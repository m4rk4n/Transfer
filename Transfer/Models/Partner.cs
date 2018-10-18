using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Models
{
    public class Partner
    {
        public Partner()
        {
            AgencyPartners = new List<AgencyPartner>();  // in case i forget to instantiate the list when adding to it
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string City { get; set; }
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } // can partner have many vehicles?

        public ICollection<AgencyPartner> AgencyPartners { get; set; }
    }
}
