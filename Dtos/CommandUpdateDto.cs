

using System.ComponentModel.DataAnnotations;

namespace dotnet_core_mvc_rest_api.Dtos
{
    public class CommandUpdateDto
    {
        // public int Id { get; set; } ... ID is created in Database
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}