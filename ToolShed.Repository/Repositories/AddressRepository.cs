using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using Toolshed.Models.SQL;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class AddressRepository
    {
        private readonly ToolShedContext toolShedContext;

        public AddressRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAddressAsync(Address address)
        {
            await toolShedContext.AddressSet
                .AddAsync(address);
            await toolShedContext.SaveChangesAsync();

            return address.AddressId;
        }

        public async Task<Address> GetAddressByAddressIdAsync(Guid addressId)
        {
            return await toolShedContext.AddressSet
                .FirstOrDefaultAsync(c => c.AddressId.Equals(addressId));
        }

        public async Task<IEnumerable<Address>> GetAddressesByStateAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(address.State))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByCityAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(address.City))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByZipCodeAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(address.ZipCode))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressOfDispensersByStateAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(address.State))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByCityAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(address.City))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByStateAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(address.State))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByZipCodeAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(address.ZipCode))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByCityAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(address.City))
                .Where(c => c.AddressType == AddressType.user)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByStateAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(address.State))
                .Where(c => c.AddressType == AddressType.user)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByZipCodeAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(address.ZipCode))
                .Where(c => c.AddressType == AddressType.user)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByCityAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(address.City))
                .Where(c => c.AddressType == AddressType.card)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByStateAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(address.State))
                .Where(c => c.AddressType == AddressType.card)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByZipCodeAsync(Address address)
        {
            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(address.ZipCode))
                .Where(c => c.AddressType == AddressType.card)
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
