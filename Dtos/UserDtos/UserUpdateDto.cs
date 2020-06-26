using System;
using System.ComponentModel.DataAnnotations;

namespace ApprovalApp.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(250)]
        public string EmailAddress { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}