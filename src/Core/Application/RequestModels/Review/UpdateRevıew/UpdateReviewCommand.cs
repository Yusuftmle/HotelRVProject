using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.RequestModels.Review.UpdateRevıew
{
    public class UpdateReviewCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }    
        public string Comment { get; set; }
        public int Rating { get; set; }
       
    }
}
