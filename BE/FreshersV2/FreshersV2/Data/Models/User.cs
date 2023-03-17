using Microsoft.AspNetCore.Identity;

namespace FreshersV2.Data.Models
{
    public class User : IdentityUser
    {
        // TODO: not the correct way to have roles
        // for the purpose of the hackaton we will use enum
        public Role Role { get; set; }

        public string Name { get; set; }

        public string FacultyNumber { get; set; } = null;


        public List<UserGroup> Groups = new List<UserGroup>();
    }
}
