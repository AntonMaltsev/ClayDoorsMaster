using Api.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.DAL.DTO
{
    public class ClayDoor : BaseEntity
    {
        public ClayDoor()
        {
            DoorUserIntegration = new List<DoorUserIntegration>();
        }

        public int Id { get; set; }

        [Required]
        [Index("IX_Name", IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<DoorUserIntegration> DoorUserIntegration { get; set; } // 1=>n relation
        
    }
}