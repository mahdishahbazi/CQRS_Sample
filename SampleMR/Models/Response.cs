using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMR.Models
{
    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }

    public class ApiResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
