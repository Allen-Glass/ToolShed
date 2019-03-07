using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using Toolshed.Repository.Models;
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
            if (address == null)
                throw new ArgumentNullException();

            await toolShedContext.AddressSet
                .AddAsync(address);
            await toolShedContext.SaveChangesAsync();

            return address.AddressId;
        }

        public async Task<Address> GetAddressByAddressIdAsync(Guid addressId)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentException("address guid cannot be empty");

            var address = await toolShedContext.AddressSet
                .FirstOrDefaultAsync(c => c.AddressId.Equals(addressId));

            if (address == null)
                throw new NullReferenceException();

            return address;
        }

        public async Task<IEnumerable<Address>> GetAddressesByStateAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByCityAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressByZipCodeAsync(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByStateAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByCityAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfDispensersByZipCodeAsync(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .Where(c => c.AddressType == AddressType.dispenser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByCityAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .Where(c => c.AddressType == AddressType.user)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByStateAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .Where(c => c.AddressType == AddressType.user)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfUsersByZipCodeAsync(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .Where(c => c.AddressType == AddressType.user)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByCityAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.City.Equals(city))
                .Where(c => c.AddressType == AddressType.card)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByStateAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.State.Equals(state))
                .Where(c => c.AddressType == AddressType.card)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesOfCardsByZipCodeAsync(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentNullException();

            return await toolShedContext.AddressSet
                .Where(c => c.ZipCode.Equals(zipCode))
                .Where(c => c.AddressType == AddressType.card)
                .ToListAsync();
        }

        public async Task UpdateAddressAsync(Address oldAddress, Address newAddress)
        {
            if (oldAddress == null || newAddress == null)
                throw new ArgumentNullException();

            toolShedContext.AddressSet
                .Remove(oldAddress);
            await toolShedContext.AddressSet
                .AddAsync(newAddress);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Address address)
        {
            if (address == null)
                throw new ArgumentNullException();

            toolShedContext.AddressSet
                .Remove(address);
            await toolShedContext.SaveChangesAsync();    
        }
    }
}
