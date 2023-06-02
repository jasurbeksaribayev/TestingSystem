using AspnetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.SolvedQuestions
{
    public class SolvedQuestionForViewDTO
    {
        public int Id { get; set; }
        public int QuizResultId { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
