using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class PasswordViewModel
    {
        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
