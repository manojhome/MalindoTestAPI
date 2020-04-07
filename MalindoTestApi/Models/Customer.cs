using System;
using System.ComponentModel.DataAnnotations;
using MalindoTestAPI.Common;

namespace MalindoTestAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(8)]
        public string Title { get; set; }
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
        [Required]
        [DOBDate(ErrorMessage = "Invalid date")]
        public DateTime DateOfBirth { get; set; }
        [Phone]
        [StringLength(25)]
        public string MobilePhoneNo { get; set; }
        [StringLength(255)]
        public string StreetAddress { get; set; }
        [StringLength(100)]
        public string SuburbCity { get; set; }
        [StringLength(15)]
        public string PostCode { get; set; }
    }
}