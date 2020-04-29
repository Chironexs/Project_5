using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class LogInViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required] 
        public string Password { get; set; }
    }
}
