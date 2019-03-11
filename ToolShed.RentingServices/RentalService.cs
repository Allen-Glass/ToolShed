using System;
using System.Threading.Tasks;
using ToolShed.Repository.Interfaces;

namespace ToolShed.RentingServices
{
    public class RentalService
    {
        private readonly ITenantSQLService tenantSQLService;
        private readonly ICardSQLService cardSQLService;
        private readonly IDispenserSQLService dispenserSQLService;
        private readonly IItemSQLService toolSQLService;

        public RentalService(ICardSQLService cardSQLService
            , ITenantSQLService tenantSQLService
            , IDispenserSQLService dispenserSQLService
            , IItemSQLService toolSQLService)
        {
            this.cardSQLService = cardSQLService;
            this.tenantSQLService = tenantSQLService;
            this.dispenserSQLService = dispenserSQLService;
            this.toolSQLService = toolSQLService;
        }

        public async Task RentItemAsync()
        {

        }
    }
}
