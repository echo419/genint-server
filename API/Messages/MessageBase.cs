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
        static readonly object thisLock = new object();


        private static int counter = 0;

        public int Counter
        {
            get
            {
                lock (thisLock)
                {
                    return counter++;
                }
            }
            set
            {

            }
        }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public int CallbackIndex { get; set; }
        public String? Exception { get; set; }
        public DateTime ServerTimeStamp
        {
            get
            {
                return DateTime.Now;
            }
            set
            {

            }
        }

    }

    public class ResponseBase<T>: ResponseBase
    {
        public T Record { get; set; }
    }

    public class FilterRequest<T, T2> where T : ViewModelBase where T2 : ExternalFilterBase
    {

        public T Record { get; set; }
        public T2 ExternalFilters { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

    }

    public class FilterResponse<T> where T : ViewModelBase
    {

        public List<T> Records { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public FilterResponse()
        {
            Records = new List<T>();
        }

    }

    public class InsertOrUpdateRequest<T> where T : ViewModelBase
    {
        public T Record { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class DeleteRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}