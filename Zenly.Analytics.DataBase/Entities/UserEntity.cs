using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zenly.Analytics.DataBase.Entities
{
    [Table("users")]
    public class UserEntity : EntityBase
    {
        [Key]
        [Column("id")]
        public string Id { set; get; }

        [Column("name")]
        [Required]
        public string Name { set; get; }

        [Column("profile_url")]
        public string ProfileUrl { set; get; }

        [Column("bio")]
        public string Bio { set; get; }

        [ForeignKey("id")]
        [Column("token_id")]
        public long TokenId { set; get; }

        public TokenEntity Token { set; get; }
    }
}
