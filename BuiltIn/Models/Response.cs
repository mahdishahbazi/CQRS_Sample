using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltIn.Models
{
    public class ApiResponse<T>: ApiResponse
    {
        public T Result { get; set; }
    }

    public class ApiResponse
    {
        public string Message { get; set; }
    }
}
