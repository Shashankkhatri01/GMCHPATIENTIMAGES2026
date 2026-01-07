using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesDtos.DTOs
{
    public class ReturnObject<T>
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public T ReturnValue { get; set; }
        public bool Status { get; set; }
    }
}
