using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Sport> Sports { get; set; }
        public string Description { get; set; }
    }
}
