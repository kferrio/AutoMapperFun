using System.Collections.Generic;
using System.Web.Mvc;
using Dropdowns.Models;
using System.Linq;

namespace Dropdowns.Controllers
{
    public class ProfileController : Controller
    {
        //
        // 1. Action method for displaying the 'User Profile' page
        //
        public ActionResult UserProfile()
        {
            // Get existing user profile object from the session or create a new one
            var model = Session["UserProfileModel"] as UserProfileModel ?? new UserProfileModel();

            // Simulate getting states from a database
            model.States = GetStatesFromDB();
            model.Roles = GetRolesFromDB();
            model.FacilityList = GetFacilityListFromDB();

            return View(model);
        }

        //
        // 2. Action method for handling user-entered data when 'Update' button is pressed.
        //
        [HttpPost]
        public ActionResult UserProfile(UserProfileModel model)
        {
            // Set States on the model. We need to do this because only the value selected 
            // in the DropDownList is posted back, not the whole list of states
            model.States = GetStatesFromDB();
            model.Roles = GetRolesFromDB();
            model.FacilityList = GetFacilityListFromDB();

            // In case everything is fine - i.e. "FirstName", "LastName" and "State" are entered/selected,
            // redirect a user to the "ViewProfile" page, and pass the user object along via Session
            if (ModelState.IsValid)
            {
                Session["UserProfileModel"] = model;
                return RedirectToAction("ViewProfile");
            }

            // Something is not right - re-render the registration page, keeping user-entered data
            // and display validation errors
            return View(model);
        }

        //
        // 3. Action method for displaying 'ViewProfile' page
        //
        public ActionResult ViewProfile()
        {
            // Get user profile information from the session
            var model = Session["UserProfileModel"] as UserProfileModel;
            if (model == null)
                return RedirectToAction("UserProfile");

            // Get a human-readable description of a currently selected State
            var allStates = GetStatesFromDB();
            model.StateName = allStates[model.State];

            var allRoles = GetRolesFromDB();
            model.RoleName = allRoles[model.Role];

            var allFacilities = GetFacilityListFromDB();
            model.FacilitiesNames = (model.Facilities).Where(allFacilities.ContainsKey).Select(i => allFacilities[i]).ToArray();

            // Display View Profile page that shows FirstName, Last Name and a selected State.
            return View(model);
        }

        /// <summary>
        /// Simulates retrieval of country's states from a DB.
        /// </summary>
        /// <returns>Dictionary of US states</returns>
        private Dictionary<string, string> GetStatesFromDB()
        {
            return new Dictionary<string, string>
            {
                {"AK", "Alaska"},
                {"AL", "Alabama"},
                {"AR", "Arkansas"},
                {"AZ", "Arizona"},
                {"CA", "California"},
                {"CO", "Colorado"},
                {"CT", "Connecticut"},
                {"DC", "District Of Columbia"},
                {"DE", "Delaware"},
                {"FL", "Florida"},
                {"GA", "Georgia"},
                {"HI", "Hawaii"},
                {"IA", "Iowa"},
                {"ID", "Idaho"},
                {"IL", "Illinois"},
                {"IN", "Indiana"},
                {"KS", "Kansas"},
                {"KY", "Kentucky"},
                {"LA", "Louisiana"},
                {"MA", "Massachusetts"},
                {"MD", "Maryland"},
                {"ME", "Maine"},
                {"MI", "Michigan"},
                {"MN", "Minnesota"},
                {"MO", "Missouri"},
                {"MS", "Mississippi"},
                {"MT", "Montana"},
                {"NC", "North Carolina"},
                {"ND", "North Dakota"},
                {"NE", "Nebraska"},
                {"NH", "New Hampshire"},
                {"NJ", "New Jersey"},
                {"NM", "New Mexico"},
                {"NV", "Nevada"},
                {"NY", "New York"},
                {"OH", "Ohio"},
                {"OK", "Oklahoma"},
                {"OR", "Oregon"},
                {"PA", "Pennsylvania"},
                {"RI", "Rhode Island"},
                {"SC", "South Carolina"},
                {"SD", "South Dakota"},
                {"TN", "Tennessee"},
                {"TX", "Texas"},
                {"UT", "Utah"},
                {"VA", "Virginia"},
                {"VT", "Vermont"},
                {"WA", "Washington"},
                {"WI", "Wisconsin"},
                {"WV", "West Virginia"},
                {"WY", "Wyoming"}
            };
        }

        /// <summary>
        /// Simulates retrieval of country's states from a DB.
        /// </summary>
        /// <returns>Dictionary of US states</returns>
        private Dictionary<string, string> GetRolesFromDB()
        {
            return new Dictionary<string, string>
            {
                {"6771570A-75E0-4217-9222-70EEC716F46A", "User"},
                {"C7F6D623-8F51-45F6-8F43-E26F003C1B7E", "Data Manager"},
                {"F9994BA5-96F8-464A-B0A8-2E093685EE71", "Administrator"},
            };
        }

        /// <summary>
        /// Simulates retrieval of country's states from a DB.
        /// </summary>
        /// <returns>Dictionary of US states</returns>
        private Dictionary<string, string> GetFacilityListFromDB()
        {
            return new Dictionary<string, string>
            {
                {"820C87A6-9D97-4DD1-B2F6-B95343AC7501", "Facility 1"},
                {"820C87A6-9D97-4DD1-B2F6-B95343AC7502", "Facility 2"},
                {"820C87A6-9D97-4DD1-B2F6-B95343AC7503", "Facility 3"},
            };
        }
    }
}