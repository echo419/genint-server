using Core.Models;

namespace API.Messages
{
    public class ApiAddContentElementView : ResponseBase
    {

        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Icon { get; set; }
        public List<ApiAddContentElementView> Children { get; set; } = [];
    }
}
