using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActionTaken { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }

    }
}
