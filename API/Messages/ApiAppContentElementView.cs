using Core.Models;

namespace API.Messages
{
    public class ApiAppContentElementView : ResponseBase
    {

        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Icon { get; set; }
        public List<ApiAppContentElementView> Children { get; set; } = [];
    }
}
