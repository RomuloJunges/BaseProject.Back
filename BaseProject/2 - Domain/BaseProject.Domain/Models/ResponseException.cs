using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Models
{
    public class ResponseException : Exception, ISerializable
    {
        private readonly HttpStatusCode _status;

        public HttpStatusCode Status
        {
            get { return _status; }
        }

        public ResponseException(HttpStatusCode status, string message) : base(message)
        {
            _status = status;
        }
    }
}
