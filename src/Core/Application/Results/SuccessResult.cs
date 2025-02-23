using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public class SuccessResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "İşlem başarılı!";
    }
}
