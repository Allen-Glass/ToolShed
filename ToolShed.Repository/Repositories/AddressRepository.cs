using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Models.Repository;
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

        public async Task<Guid> AddAsync(Address address, CancellationToken cancellationToken = default)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            await toolShedContext.AddressSet
                .AddAsync(address, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return address.AddressId;
        }

        public async Task<string> GetStateAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentNullException(nameof(addressId));

            var state = await toolShedContext.AddressSet
                .Where(c => c.AddressId.Equals(addressId))
                .Select(c => c.State)
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrEmpty(state))
                throw new NullReferenceException();

            return state;
        }

        public async Task<Address> GetAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentNullException(nameof(addressId));

            var address = await toolShedContext.AddressSet
                .FirstOrDefaultAsync(c => c.AddressId.Equals(addressId), cancellationToken);

            if (address == null)
                throw new NullReferenceException();

            return address;
        }

        public async Task<IEnumerable<Address>> ListAsync(IEnumerable<Guid> addressIds, CancellationToken cancellationToken = default)
        {
            if (addressIds == null)
                throw new ArgumentNullException(nameof(addressIds));

            var addressList = new List<Address>();
            foreach (var addressId in addressIds)
            {
                addressList.Add(await GetAsync(addressId, cancellationToken));
            }

            return addressList;
        }

        public async Task<IEnumerable<Address>> GetAddressesByStateAsync(string state, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressByCityAsync(string city, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException(nameof(city));

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException(nameof(zipCode));

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByStateAsync(string state, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .Where(c => c.AddressType == AddressType.Dispenser)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByCityAsync(string city, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException(nameof(city));

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .Where(c => c.AddressType == AddressType.Dispenser)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException(nameof(zipCode));

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .Where(c => c.AddressType == AddressType.Dispenser)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByCityAsync(string city, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException(nameof(city));

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .Where(c => c.AddressType == AddressType.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByStateAsync(string state, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .Where(c => c.AddressType == AddressType.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException(nameof(zipCode));

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .Where(c => c.AddressType == AddressType.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByCityAsync(string city, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException(nameof(city));

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .Where(c => c.AddressType == AddressType.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByStateAsync(string state, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .Where(c => c.AddressType == AddressType.Card)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException(nameof(zipCode));

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .Where(c => c.AddressType == AddressType.Card)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Address oldAddress, Address newAddress, CancellationToken cancellationToken = default)
        {
            if (oldAddress == null)
                throw new ArgumentNullException(nameof(oldAddress));

            if (newAddress == null)
                throw new ArgumentNullException(nameof(newAddress));

            //get old address dto, update with new address properties then save change
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Address address, CancellationToken cancellationToken = default)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            toolShedContext.AddressSet
                .Remove(address);
            await toolShedContext.SaveChangesAsync(cancellationToken);    
        }
    }
}
