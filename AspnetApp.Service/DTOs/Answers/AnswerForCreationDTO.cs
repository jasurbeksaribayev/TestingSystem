using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Answers
{
    public class AnswerForCreationDTO
    {
        public string Content { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; } 
        public int QuestionId { get; set; }
    }
}
