using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models;


namespace Core.ViewModels
{

    public class UserView : ViewModelBase
    {
        public string UserName { get; set; } = string.Empty;
        public string NickText { get; set; } = string.Empty;
        public string? NickJson { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? LastConnectedAt { get; set; }
        public DateTime? LastDisconnectedAt { get; set; }
        public int? IsBot { get; set; }
        public int? IsMuted { get; set; }
        public string? Json { get; set; }

    }

}
