using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PlateNumber { get; set; }
        public string Note { get; set; }

        public int? LastServiceTransferId { get; set; }
        public Transfer LastService { get; set; }
        public Partner Partner { get; set; }

    }
}
