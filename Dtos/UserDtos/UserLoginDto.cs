using System.ComponentModel.DataAnnotations;

namespace ApprovalApp.Dtos.UserDtos
{
    public class UserLoginDto
    {
        [Required]
        [MaxLength(250)]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}