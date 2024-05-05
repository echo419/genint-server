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
        public string PasswordHash { get; set; } = string.Empty;
    }

}
