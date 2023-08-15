namespace Core.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
