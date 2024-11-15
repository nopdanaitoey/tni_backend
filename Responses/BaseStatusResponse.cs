using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tni_back.Responses
{
    public class BaseStatusResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}