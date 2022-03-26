using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Zenly.Analytics.DataBase.Entities
{
    public class EntityBase : DbSet
    {
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { set; get; }

        [Column("updated_at")]
        public DateTime UpdatedAt { set; get; }
    }
}
