using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Messages
{
    public class RequestBase<T>
    {

        public int CallbackIndex { get; set; }
        public T Data { get; set; }
        public string Type { get; set; }

    }
}