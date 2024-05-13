using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.DTO
{
    public class ResponseObject<T>
    {
        public string Message { get; set; }

        public List<T> Data { get; set; }

        public int StatusCode { get; set; }
        
    }
}
