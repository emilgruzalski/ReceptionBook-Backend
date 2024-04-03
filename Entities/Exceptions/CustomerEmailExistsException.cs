using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CustomerEmailExistsException : BadRequestException
    {
        public CustomerEmailExistsException(string email)
        : base($"The email {email} already exists in the database.")
        {
        }
    }
}
