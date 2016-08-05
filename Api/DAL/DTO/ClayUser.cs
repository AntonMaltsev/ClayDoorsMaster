using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.DAL.DTO
{
    public class ClayUser : BaseEntity
    {
        public ClayUser()
        {
            DoorUserIntegration = new List<DoorUserIntegration>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IX_Name", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string LoginName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Token { get; set; }
   
        public virtual ICollection<DoorUserIntegration> DoorUserIntegration { get; set; } // 1=>n relation

    }
}