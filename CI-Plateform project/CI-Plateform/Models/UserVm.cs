using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Models
{
    public class UserVm
    {
        public User? User { get; set; } = null!;
        public City? City { get; set; } = null;
        public Country? Country { get; set; } = null;
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
