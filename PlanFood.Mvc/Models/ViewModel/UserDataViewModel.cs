using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class UserDataViewModel
    {
        [Required]
        public string UserName
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
    }
}
