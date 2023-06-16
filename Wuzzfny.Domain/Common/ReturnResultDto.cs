using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wuzzfny.Domain.Common
{
    public class ReturnResultDto<T>
    {
        public HttpStatusCode httpStatusCode { get; set; } = HttpStatusCode.OK;
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
