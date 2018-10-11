using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.Repositories.Interfaces;
using Transfer.Models;

namespace Transfer.DAL.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransferContext ctx) : base(ctx)
        {

        }
    }
}
