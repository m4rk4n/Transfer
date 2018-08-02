using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.DAL
{
    public class TransferRepository  : ITransferRepository
    {
        private readonly TransferContext ctx;
        private readonly ILogger<TransferRepository> logger;

        public TransferRepository(TransferContext ctx, ILogger<TransferRepository> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }
    }
}
