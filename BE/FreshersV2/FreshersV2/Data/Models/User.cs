﻿using Microsoft.AspNetCore.Identity;

namespace FreshersV2.Data.Models
{
    public class User : IdentityUser
    {
        // TODO: not the correct way to have roles
        // for the purpose of the hackaton we will use enum
        public Role Role { get; set; } = Role.User;

        public string FacultyNumber { get; set; } = null;

        public int? GroupId { get; set; }

        public Group? Group { get; set; }
    }
}
