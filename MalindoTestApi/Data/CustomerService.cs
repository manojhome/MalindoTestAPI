using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalindoTestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MalindoTestAPI.Data
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAll();
        public Task<Customer> GetById(int id);
        public Task<int> Put(int id, Customer customer);
        public Task<int> Add(Customer customer);
        public Task<Customer> Remove(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _context;

        public CustomerService(CustomerContext context)
        {
            _context = context;
        }
        
        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task<int> Put(int id, Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Log.Logger.Error("DB Error when updating customer data for customer id : {@CustomerId} {@Error}", id, ex);

                if (!CustomerExists(id))
                {
                    return -1;
                }

                throw;
            }
        }

        public async Task<int> Add(Customer customer)
        {
            try
            {
                _context.Customer.Add(customer);
                var resultSave = await _context.SaveChangesAsync();
                return resultSave;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("DB Error when adding customer data {@Error}", ex);
                return -1;
            }
        }
        
        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return null;
            }

            try
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("DB Error when deleting customer data for customer id : {@CustomerId} {@Error}", id, ex);
                return null;
            }
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
