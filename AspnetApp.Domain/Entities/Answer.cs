using AspnetApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Domain.Entities
{
    public class Answer : Auditable
    { 
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
        public ICollection<SolvedQuestion> SolvedQuestions { get; set; }
    }
}
