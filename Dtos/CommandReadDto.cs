namespace dotnet_core_mvc_rest_api.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }
        // public string Platform { get; set; }   ...  this will be ommited from response
    }
}