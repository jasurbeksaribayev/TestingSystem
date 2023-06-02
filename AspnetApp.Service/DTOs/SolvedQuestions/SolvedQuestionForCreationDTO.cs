using AspnetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.SolvedQuestions
{
    public class SolvedQuestionForCreationDTO
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool CorrectAnswer { get; set; }
        public int QuizRezultId { get; set; }
    }
}
