using Newtonsoft.Json;

namespace UserService.Models
{
    public class RemoveUser
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
    }
}
