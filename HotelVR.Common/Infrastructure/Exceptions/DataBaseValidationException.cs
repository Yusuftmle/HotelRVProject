using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Exceptions
{
    public class DataBaseValidationException:Exception
    {

        public DataBaseValidationException()
        {
        }

        public DataBaseValidationException(string? message) : base(message)
        {
        }

        public DataBaseValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

       
    }
}
