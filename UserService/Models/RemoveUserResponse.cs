using Newtonsoft.Json;

namespace UserService.Models
{
    public class RemoveUserResponse
    {
        [JsonProperty("Msg")]
        public string Message { get; set; }
        public bool Success { get; set; }
        public User User { get; set; }
    }
}
