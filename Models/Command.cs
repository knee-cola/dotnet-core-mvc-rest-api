using System.ComponentModel.DataAnnotations;

namespace dotnet_core_mvc_rest_api.Models {
    public class Command
    {
        // we don't really need to place `[Key]` since `Id` is `[Key]` and `[Required]` by default (automatically)
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }
        
        [Required]
        public string Platform { get; set; }
    }
}