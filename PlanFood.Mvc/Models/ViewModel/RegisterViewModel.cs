using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Login
        {
            get { return this.Email; }
            set { this.Email = value; }
        }
        
        [Required] 
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required] 
        public string Password { get; set; }
        
        [Required]
        public string RepeatPassword { get; set; }
    }
}
