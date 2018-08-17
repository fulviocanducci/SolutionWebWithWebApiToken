using System.ComponentModel.DataAnnotations;

namespace WebAppFullFrameworkApi.Models
{
    public sealed class User
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}