using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppContentElement : ModelBase
    {
        public int? ParentId { get; set; }
        public virtual AppContentElement? Parent { get; set; }
        public virtual IEnumerable<AppContentElement>? Children { get; set; }

        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Icon {  get; set; }


    }
}
