using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.RequestModels.User.PasswordComment
{
    public class ChangePasswordCommand:IRequest<bool>
    {
        public ChangePasswordCommand(Guid? userId, string oldPassword, string newPassword)
        {
            UserId = userId;
            this.oldPassword = oldPassword;
            this.newPassword = newPassword;
        }

        public Guid? UserId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

       
    }
}
