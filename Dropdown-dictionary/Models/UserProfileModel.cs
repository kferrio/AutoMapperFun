using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dropdowns.Models
{
    public class UserProfileModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        // This property holds user-selected state
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Facilities")]
        public IEnumerable<string> Facilities { get; set; }


        // This property holds all available states for selection
        public Dictionary<string, string> States { get; set; }
        public Dictionary<string, string> Roles { get; set; }
        public Dictionary<string, string> FacilityList { get; set; }

        // Property to store human-readable state name
        public string StateName { get; set; }
        public string RoleName { get; set; }
        public string[] FacilitiesNames { get; set; }
        public string GetName {
            get
            {
                return $"{FirstName.Trim()} {LastName.Trim()}";
            }
        }
    }
}
