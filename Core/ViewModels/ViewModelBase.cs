using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.ViewModels
{

    public class ViewModelBase
    {

        protected int BoolToInt(bool? input)
        {
            if (input.HasValue && input.Value == true) return 1;
            return 0;
        }

        protected bool IntToBool(int? input)
        {
            if (input.HasValue && input.Value == 1) return true;
            return false;
        }

        public int? Id { get; set; } // can be null when inserting
        public DateTime? AddTime { get; set; }

    }

}