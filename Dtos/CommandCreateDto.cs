namespace dotnet_core_mvc_rest_api.Dtos
{
    public class CommandCreateDto
    {
        // public int Id { get; set; }    ... ID is created in Database
        public string HowTo { get; set; }
        public string Line { get; set; }
        public string Platform { get; set; }
    }
}