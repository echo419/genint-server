using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace API.Messages
{

    public class ExternalFilterBase
    {

    }

    public class ResponseBase
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }

    public class ResponseBase<T>: ResponseBase
    {
        public T? Record { get; set; }
    }



}