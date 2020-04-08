using System;
using System.Collections.Generic;
using MalindoTestAPI.Controllers;
using MalindoTestAPI.Data;
using MalindoTestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using MalindoTestAPI.Auth;
using NSubstitute;

namespace MalindoTestApi.Tests
{
    public class CustomerControllerTests
    {        
        private readonly ICustomerService _service;
        private readonly IAuthentication _auth;

        private CustomersController _controller;

        public CustomerControllerTests()
        {
            _service = new CustomerServiceMock();
            _auth = Substitute.For<IAuthentication>();
            _controller = new CustomersController(_service, _auth);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);

            // Act
            var okResult = _controller.GetCustomer().Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);

            // Act
            var okResult = _controller.GetCustomer().Result ;

            // Assert
            var items = Assert.IsType<List<Customer>>(((ObjectResult)okResult.Result).Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);

            // Act
            var notFoundResult = _controller.GetCustomer(5).Result;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var testId = 1;

            // Act
            var okResult = _controller.GetCustomer(testId).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var testId = 2;

            // Act
            var okResult = _controller.GetCustomer(testId).Result;

            // Assert
            var value = ((ObjectResult) okResult.Result).Value;
            Assert.IsType<Customer>(value);
            Assert.Equal(testId, ((Customer) value).CustomerId);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var nameMissingItem = new Customer()
            {
                PostCode = "23423",
                StreetAddress = "asdasdada"
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.PostCustomer(nameMissingItem).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            Customer testItem = new Customer()
            {
                Title = "Mr",
                FirstName = "Guinness",
                LastName = "Testabc",
                DateOfBirth = DateTime.Now.AddYears(-30),
                EmailAddress = "sadasdads@sdadasdd.com"
            };

            // Act
            var createdResponse = _controller.PostCustomer(testItem).Result;

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var testItem = new Customer()
            {
                Title = "Mr",
                FirstName = "Guinness",
                LastName = "Testabc",
                DateOfBirth = DateTime.Now.AddYears(-30),
                EmailAddress = "sadasdads@sdadasdd.com"
            };

            // Act
            var createdResponse = _controller.PostCustomer(testItem).Result;
            var item = ((ObjectResult)createdResponse.Result).Value as Customer;
            // Assert

            Assert.IsType<Customer>(item);
            Assert.Equal("Testabc", item.LastName);
        }


        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var notExistingId = 5;

            // Act
            var badResponse = _controller.DeleteCustomer(notExistingId).Result;

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var existingId = 2;

            // Act
            var okResponse = _controller.DeleteCustomer(existingId).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResponse.Result);
        }
        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange 
            _auth.IsAuthenticated(null).Returns(true);
            var existingId = 2;

            // Act
            var okResponse = _controller.DeleteCustomer(existingId);

            // Assert
            Assert.Equal(2, _service.GetAll().Result.Count);
        }
    }
}
