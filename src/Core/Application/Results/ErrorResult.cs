using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public class ErrorResult
    {
        public bool Error { get; set; } = true;
        public string Message { get; set; } = "İşlem hatali!";
    }
}
