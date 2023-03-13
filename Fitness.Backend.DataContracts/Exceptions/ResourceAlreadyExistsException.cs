using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public override string Message => string.Format("The resource - ({0}) - you're trying to create already exists",Resource);

        public string? Resource { get; set; }

        public ResourceAlreadyExistsException(string resource)
        {
            Resource = resource;
        }
        public ResourceAlreadyExistsException()
        {

        }
    }
}
