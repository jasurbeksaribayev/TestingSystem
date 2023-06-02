using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Answers
{
    public class AnswerForUpdateDTO
    {
        public string Content { get; set; }
        public string Option { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
