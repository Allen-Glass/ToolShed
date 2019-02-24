using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.User;
using ToolShed.Repository.Context;

namespace ToolShed.Repository
{
    public class AddressRepository
    {
        private readonly ToolShedContext toolShedContext;

        public AddressRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAddressAsync(Address address)
        {
            await toolShedContext.AddressSet
                .AddAsync(address);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<Address> GetAddressByAddressId(Guid addressId)
        {
            return await toolShedContext.AddressSet
                .FirstOrDefaultAsync(c => c.AddressId.Equals(addressId));
        }

        public async Task<IEnumerable<Address>> GetAddressesByState(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(address.State))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByCity(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(address.City))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByZipCode(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(address.ZipCode))
                .ToListAsync();
        }

        public async Task UpdateAddressAsync(Address oldAddress, Address newAddress)
        {
            toolShedContext.AddressSet
                .Remove(oldAddress);
            await toolShedContext.AddressSet
                .AddAsync(newAddress);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Address address)
        {
            toolShedContext.AddressSet
                .Remove(address);
            await toolShedContext.SaveChangesAsync();    
        }
    }
}
