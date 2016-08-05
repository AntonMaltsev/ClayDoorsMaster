using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class DoorViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }

    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int LoginName { get; set; }

    }

    public class DoorUserViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int LoginName { get; set; }

        public IEnumerable<UserViewModel> ClayUsers { get; set; }
    }

    public class UserDoorViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int LoginName { get; set; }

        public IEnumerable<DoorViewModel> ClayDoors { get; set; }
    }
}