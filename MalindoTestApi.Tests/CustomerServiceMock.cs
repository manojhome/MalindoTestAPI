using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalindoTestAPI.Data;
using MalindoTestAPI.Models;

namespace MalindoTestApi.Tests
{
    public class CustomerServiceMock : ICustomerService
    {
        private readonly List<Customer> _customer;
        public CustomerServiceMock()
        {
            _customer = new List<Customer>()
            {
                new Customer() {CustomerId = 1,DateOfBirth = DateTime.Now.AddYears(-20),EmailAddress = "manojtest12@malidon.com",FirstName = "Manojs",LastName = "Sugatadasas", MobilePhoneNo = "31212312311",PostCode = "2321",StreetAddress = "Test address121",SuburbCity = "Melbourne",Title = "Mr"},
                new Customer() {CustomerId = 2,DateOfBirth = DateTime.Now.AddYears(-30),EmailAddress = "manojtest23@malidon.com",FirstName = "Manojd",LastName = "Sugatadasad", MobilePhoneNo = "31212312312",PostCode = "2322",StreetAddress = "Test address122",SuburbCity = "Melbourne",Title = "Mrs"},
                new Customer() {CustomerId = 3,DateOfBirth = DateTime.Now.AddYears(-40),EmailAddress = "manojtest45@malidon.com",FirstName = "Manojf",LastName = "Sugatadasaf", MobilePhoneNo = "31212312313",PostCode = "2323",StreetAddress = "Test address123",SuburbCity = "Melbourne",Title = "Ms"},

            };
        }

        public async Task<List<Customer>> GetAll()
        {
            await Task.Delay(1000);
            return _customer;
        }

        public async Task<Customer> GetById(int id)
        {

            var customer=  _customer.FirstOrDefault(a => a.CustomerId == id);
            if (customer == null)
            {
                return null;
            }

            return (customer);
        }

        public async Task<int> Put(int id, Customer customer)
        {
            var existing = _customer.First(a => a.CustomerId == id);
            if (existing == null)
            {
                return -1;
            }
            _customer.Remove(existing);
            _customer.Add(customer);
            return 1;
        }

        public async Task<int> Add(Customer customer)
        {
            customer.CustomerId = 4;
            _customer.Add(customer);
            return 1;
        }

        public async Task<Customer> Remove(int id)
        {
           var existing = _customer.FirstOrDefault(a => a.CustomerId == id);
           if (existing == null)
           {
               return null;
           }
           _customer.Remove(existing);
           return existing;
        }
    }
}
