using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.RequestModels.User.CreateUser
{
    public class CreateUserCommand:IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; } // Kullanıcı tam adı
        public string PhoneNumber { get; set; } // Telefon numarası
        public string Password { get; set; }
      
    }
}
