using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public override string Message => "The resource you're trying to access does not exist";
    }
}
