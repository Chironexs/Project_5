using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class ContactViewModel
    {       
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string PostalCodeAndCity { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyEmailAddress { get; set; }
    }
}
