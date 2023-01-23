using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Plateform.Models
{
    public class ShareStory
    {
        public Mission? mission { get; set; }
        public IEnumerable<SelectListItem> MissionList { get; set; }
        public StoryMedium? storyMedium { get; set; }
        public Story? Story { get; set; }
        public User? user { get; set; }
    }
}
