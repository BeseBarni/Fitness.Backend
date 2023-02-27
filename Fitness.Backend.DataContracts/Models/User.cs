using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class User
    {
        public string Id { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
