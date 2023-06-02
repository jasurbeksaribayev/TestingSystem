using AspnetApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Domain.Entities
{
    public class Quiz : Auditable
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Title { get; set; }
        public ICollection<QuizRezult> QuizRezults { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
