using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.Repositories;
using Transfer.DAL.Repositories.Interfaces;
using Transfer.Models;

namespace Transfer.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly TransferContext ctx;

        public IRepository<Partner> partnerRepository;
        public IRepository<Transfer.Models.Transfer> transferRepository;
        public IRepository<Vehicle> vehicleRepository;
        public IRepository<Agency> agencyRepository;
        public UserRepository userRepository;
        public UserManager<TransferUser> userManager {get;}

        //public UnitOfWork()
        //{
        //    //ctx = context ?? throw new ArgumentNullException("Context argument cannot be null in UoW");
        //    // If you have collections in entites, include them here
        //    partnerRepository = new PartnerRepository(ctx);
        //    transferRepository = new TransferRepository(ctx);
        //    vehicleRepository = new VehicleRepository(ctx);
        //    agencyRepository = new AgencyRepository(ctx);
        //}

        public UnitOfWork(TransferContext context)
        {
            ctx = context ?? throw new ArgumentNullException("Context argument cannot be null in UoW");
            // If you have collections in entites, include them here
            partnerRepository = new PartnerRepository(ctx);
            transferRepository = new TransferRepository(ctx);
            vehicleRepository = new VehicleRepository(ctx);
            agencyRepository = new AgencyRepository(ctx);
        }

        public UnitOfWork(UserManager<TransferUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
        //    userRepository = new UserRepository(userManager, roleManager);
        }

        public bool Complete()
        {
            return ctx.SaveChanges() > 0; 
        }

        public async Task<bool> CompleteAsync()
        {
            return await ctx.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            if (this.ctx != null)
                ctx.Dispose();
        }

    }
}
