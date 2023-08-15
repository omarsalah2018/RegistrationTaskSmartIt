using System.ComponentModel.DataAnnotations;

namespace RegistrationTask.ViewModels

{
    public class AddUserVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get;set; }
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,8}$", ErrorMessage = "Not a valid Mobile number")]

        public string Mobile { get;set; }
        [Required]

        public DateTime BirthDate { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
