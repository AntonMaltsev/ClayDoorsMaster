using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DAL.DTO
{
    public class BaseEntity
    {
        [Required(ErrorMessage = "CreatedUTC must have not null value")]
        public DateTime CreatedUTC { get; set; }

        public DateTime? UpdatedUTC { get; set; }

        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            CreatedUTC = DateTime.UtcNow;
        }

        [NotMapped]
        public DateTime UpdatedCreated
        {
            get
            {
                return UpdatedUTC.HasValue ? UpdatedUTC.Value : CreatedUTC;
            }
        }
    }
}