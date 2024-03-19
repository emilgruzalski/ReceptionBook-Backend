using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class EndDateRangeBadRequestException : BadRequestException
    {
        public EndDateRangeBadRequestException() : base("End date cannot be earlier than the start date.")
        {
        }
    }
}
