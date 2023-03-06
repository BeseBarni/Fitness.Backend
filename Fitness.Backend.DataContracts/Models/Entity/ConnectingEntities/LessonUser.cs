using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Backend.Application.DataContracts.Models.Entity.ConnectingEntities
{
    public class LessonUser
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
