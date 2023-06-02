using AspnetApp.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Domain.Entities
{
    public class SolvedQuestion : Auditable
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public bool CorrectAnswer { get; set; }
        public int QuizRezultId { get; set; }
        public QuizRezult QuizRezult { get; set; }
    }
}
