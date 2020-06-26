namespace ApprovalApp.Dtos.UserDtos
{
    public class UserReadDto
    {

        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}