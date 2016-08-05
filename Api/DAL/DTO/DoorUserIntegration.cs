using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.DAL.DTO
{
    public class DoorUserIntegration : BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Desc { get; set; }

        public int ClayUserId { get; set; } // foreign key 
        public virtual ClayUser ClayUser { get; set; } // navigation property

        public int ClayDoorId { get; set; } // foreign key 
        public virtual ClayDoor ClayDoor { get; set; } // navigation property
    }
}