using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models
{
    public class Lesson
    {
        public string Name { get; set; }
        public string SportType { get; set; }
        public int InstructorId { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }

    }
}
