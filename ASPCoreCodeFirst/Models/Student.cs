using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCoreCodeFirst.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        [Column("Name",TypeName ="varchar(100)")]
        public string Name { get; set; }
        [Column("Gender", TypeName = "varchar(20)")]
        //[Required]
        public string Gender { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? Standard { get; set; }
    }
}
