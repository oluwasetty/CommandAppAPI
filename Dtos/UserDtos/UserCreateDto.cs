using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;

namespace ApprovalApp.Dtos.UserDtos
{
    public class UserCreateDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(250)]
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}