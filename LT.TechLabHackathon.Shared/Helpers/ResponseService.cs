using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.Helpers
{
    public class ResponseService<T>
    {
        public ResponseService()
        {
            Message ??= string.Empty;
        }

        public ResponseService(bool hasError, string message, HttpStatusCode statusCode, T content)
        {
            HasError = hasError;
            Message = message;
            StatusHttp = statusCode;
            Content = content;
            TotalRecords = 0;
        }

        public ResponseService(bool hasError, string message, HttpStatusCode statusCode, T content, int totalRecords)
        {
            HasError = hasError;
            Message = message;
            StatusHttp = statusCode;
            Content = content;
            TotalRecords = totalRecords;
        }

        public bool HasError { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusHttp { get; set; }
        public T? Content { get; set; }
        public int TotalRecords { get; set; }
    }
}
