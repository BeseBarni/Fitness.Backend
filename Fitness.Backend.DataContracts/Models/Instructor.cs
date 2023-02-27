using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class Instructor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public List<Sport> Sports { get; set; }
    }
}
