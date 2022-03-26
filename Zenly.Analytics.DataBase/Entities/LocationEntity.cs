using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zenly.Analytics.DataBase.Entities
{
    [Table("locations")]
    public class LocationEntity : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { set; get; }

        [ForeignKey("id")]
        [Column("user_id")]
        public string UserId { set; get; }

        public UserEntity User { set; get; }

        [Column("latitude")]
        [Required]
        public double Latitude { set; get; }

        [Column("longitude")]
        [Required]
        public double Longitude { set; get; }
    }
}