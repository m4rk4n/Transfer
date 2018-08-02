using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Models
{
    public class TransferUser : IdentityUser
    {
        public string Name { get; set; }
        
    }
}
