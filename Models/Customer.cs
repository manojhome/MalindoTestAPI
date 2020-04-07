﻿using System;
using System.Collections.Generic;

namespace MalindoTestAPI.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobilePhoneNo { get; set; }
        public string StreetAddress { get; set; }
        public string SuburbCity { get; set; }
        public string PostCode { get; set; }
    }
}