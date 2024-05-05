using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AppContentElementView : ViewModelBase
    {
        public int? ParentId { get; set; }
        public virtual AppContentElement? Parent { get; set; }

        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Icon { get; set; }
    }
}
