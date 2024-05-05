

namespace API.Messages
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public ApiAppContentElementView? AppContent { get; set; }

    }
}
