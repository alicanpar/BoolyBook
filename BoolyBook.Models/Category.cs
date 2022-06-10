using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BoolyBook.Models
{
    public class Category
    {
        [Key] //ifadeyi eklemek için sol tarafta ampul. ctrl+enter -- Birincil anahtar 
        public int Id { get; set; }
        [Required]  //gereklilik
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Değer aralığı 1,100 arası olmak zorundadır")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
