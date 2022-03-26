using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Zenly.Analytics.DataBase.Entities
{
    [Table("tokens")]
    public class TokenEntity : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { set; get; }
        
        [Column("value")]
        [Required]
        public string Value { set; get; }

        [Column("summary")]
        public string Summary { set; get; }
    }
}
