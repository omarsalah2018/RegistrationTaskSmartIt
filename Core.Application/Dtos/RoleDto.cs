using System.ComponentModel.DataAnnotations;

namespace Core.Application.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string RoleDescription { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
