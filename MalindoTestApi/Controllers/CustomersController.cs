using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MalindoTestAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MalindoTestAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using Serilog;
using MalindoTestAPI.Auth;

namespace MalindoTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IAuthentication _auth;

        public CustomersController(ICustomerService service, IAuthentication auth)
        {
            _service = service;
            _auth = auth;
        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            List<Customer> result;

            try
            {
                // simple auth. This can be properly done through some Authentication service like OAuth
                var isAuthenticated = _auth.IsAuthenticated(Request);

                if (!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                result = await _service.GetAll();                
            }
            catch(Exception ex)
            {
                Log.Logger.Error("Error when retrieving customers data {@Error}", ex);
                return NotFound("Customers Data Not Found!");
            }

            return Ok(result);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                // simple auth. This can be properly done through some Authentication service like OAuth
                var isAuthenticated = _auth.IsAuthenticated(Request);                

                if (!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customer = await _service.GetById(id);
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when retrieving customer data for customer id : {@CustomerId}  {@Error}", id, ex);
                return NotFound("Customer Not Found!");
            }
        }


        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            try
            {
                // simple auth. This can be properly done through some Authentication service like OAuth
                var isAuthenticated = _auth.IsAuthenticated(Request);

                if (!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }

                if (id != customer.CustomerId && !ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await _service.Put(id, customer);
                if (result < 0)
                {
                    return NotFound("Customer Not Found!");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when updating customer data for customer id : {@CustomerId}  {@Error}", id, ex);
                return StatusCode(500);
            }
        }        


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                // simple auth. This can be properly done through some Authentication service like OAuth
                var isAuthenticated = _auth.IsAuthenticated(Request);

                if (!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.Add(customer);
                if (result < 0)
                {
                    return NotFound();
                }

                return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when adding customer data {@Error}", ex);
                return StatusCode(500);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                // simple auth. This can be properly done through some Authentication service like OAuth
                var isAuthenticated = _auth.IsAuthenticated(Request);

                if (!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.Remove(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when deleting customer data for the customer id : {@CustomerId} {@Error}", id, ex);
                return StatusCode(500);
            }
        }
    }
}
